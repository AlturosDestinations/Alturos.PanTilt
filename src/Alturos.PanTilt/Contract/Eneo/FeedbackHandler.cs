using Alturos.PanTilt.Contract.Eneo.Response;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Alturos.PanTilt.Contract.Eneo
{
    /// <summary>
    /// Understands the feedback the head sends back to the application and 
    /// converts them into application domain knowledge
    /// </summary>
    public class FeedbackHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(FeedbackHandler));
        private readonly ConcurrentQueue<byte> _buffer = new ConcurrentQueue<byte>();

        public List<BaseResponse> HandleResponse(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return null;
            }

            //Add data to buffer
            foreach (var b in data)
            {
                this._buffer.Enqueue(b);
            }

            var items = new List<BaseResponse>(10);
            for (var i = 0; i < 10; i++)
            {
                data = this.CheckPackage();
                if (data == null)
                {
                    return items;
                }

                var response = this.ParseResponse(data);
                if (response == null)
                {
                    var hex = BitConverter.ToString(data);
                    Log.Debug($"{nameof(HandleResponse)} - Invalid answer {hex}");
                    continue;
                }
                items.Add(response);
            }

            return items;
        }

        private byte[] CheckPackage()
        {
            byte b = 0x00;

            for (var i = 0; i < 10; i++)
            {
                //To less bytes available for a full answer
                if (this._buffer.Count < 7)
                {
                    return null;
                }

                //Get the first element in queue
                if (!this._buffer.TryPeek(out b))
                {
                    return null;
                }

                //If the element not a start element, remove it
                if (b != 0xFF)
                {
                    if (!this._buffer.TryDequeue(out b))
                    {
                        return null;
                    }

                    continue;
                }

                //Is the first element a start element leave the loop
                break;
            }

            var buffer = new byte[7];
            for (var i = 0; i < 7; i++)
            {
                if (!this._buffer.TryDequeue(out b))
                {
                    return null;
                }

                buffer[i] = b;
            }

            return buffer;
        }

        private BaseResponse ParseResponse(byte[] data)
        {
            // Data structure
            // FF-01-AA-0B-46-34-30
            // │  │  │  │  │  │  │
            // │  │  │  │  │  │  └─ Checksum
            // │  │  │  │  │  └──── Data 2
            // │  │  │  │  └─────── Data 1
            // │  │  │  └────────── Status
            // │  │  └───────────── Response Type
            // │  └──────────────── Address?
            // └─────────────────── Package-Type is Answer (fixed)

            if (data == null)
            {
                return null;
            }

            //Answer is always 7 bytes
            if (data.Length != 7)
            {
                return null;
            }

            //Answer start always with FF
            if (data[0] != 0xFF)
            {
                return null;
            }

            var checksumResult = this.CalculateChecksum(data) == data[6];
            var value = this.GetPositionResponse(data[3], data[4], data[5]);

            //Response Type
            switch (data[2])
            {
                case 0xAA: //Pan Limit Min
                    return new PanLimitResponse(LimitType.Min, value, checksumResult);
                case 0xAB: //Pan Limit Max
                    return new PanLimitResponse(LimitType.Max, value, checksumResult);
                case 0xAC: //Tilt Limit Min
                    return new TiltLimitResponse(LimitType.Min, value, checksumResult);
                case 0xAD: //Tilt Limit Max
                    return new TiltLimitResponse(LimitType.Max, value, checksumResult);
                case 0xCA: //Tilt Info
                    return new TiltInfoResponse(value, checksumResult);
                case 0xCB: //Pan Info
                    return new PanInfoResponse(value, checksumResult);
                case 0xA0:
                    return new PotentiometerResponse("Pan", value, checksumResult);
                case 0xA2:
                    return new PotentiometerResponse("Tilt", value, checksumResult);
                case 0xA5:
                    value = this.GetTemperature(data[3], data[4], data[5]);
                    return new TemperatureResponse(value, checksumResult);
                case 0x6A:
                    return new LimitActiveResponse(checksumResult);
                case 0xB0:
                    var limitOverrunType = this.GetLimitOverrunType(data[3], data[4], data[5]);
                    return new LimitOverrunResponse(limitOverrunType, checksumResult);
                default:
                    var hex = BitConverter.ToString(data);
                    Log.Warn($"{nameof(ParseResponse)} - Unknown Response Type {hex}");
                    break;
            }

            return null;
        }

        public int CalculateChecksum(byte[] data)
        {
            byte checksum = 0;
            unchecked // Let overflow occur without exceptions
            {
                for (var i = 1; i < data.Length - 1; i++)
                {
                    checksum += data[i];
                }
            }

            return checksum;
        }

        public double GetPositionResponse(byte status, byte data1, byte data2)
        {
            if ((status & 0x04) == 0x04) // restore dat2 as 0xff
            {
                data2 = 0xFF;
            }

            var position = (256 * data1 + data2);

            if ((status & 0x01) == 0x01)
            {
                position *= -1;
            }

            return position / 100.0;
        }

        public double GetTemperature(byte status, byte data1, byte data2)
        {
            return 402.6 * (256 * data1 + data2) / 4096 - 278;
        }

        public LimitOverrunType GetLimitOverrunType(byte status, byte data1, byte data2)
        {
            if (status != 0x4C) //L
            {
                Log.Error($"{nameof(GetLimitOverrunType)} - status is invalid, must be 0x4C reveived 0x{status:x2}");
                return LimitOverrunType.Unknown;
            }

            if (data1 == 0x54 && data2 == 0x54) //T
            {
                return LimitOverrunType.Tilt;
            }
            else if (data1 == 0x50 && data2 == 0x50) //P
            {
                return LimitOverrunType.Pan;
            }
            else if (data1 == 0x50 && data2 == 0x54) //P & T
            {
                return LimitOverrunType.Both;
            }

            Log.Error($"{nameof(GetLimitOverrunType)} - data1 & data2 are invalid, received 0x{data1:x2} 0x{data2:x2}");
            return LimitOverrunType.Unknown;
        }
    }
}
