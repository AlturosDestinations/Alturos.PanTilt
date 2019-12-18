using Alturos.PanTilt.Communication;
using log4net;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alturos.PanTilt
{
    public class AlturosPanTiltControl : IPanTiltControl, IFirmwareReader
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AlturosPanTiltControl));

        public event Action<PanTiltPosition> PositionChanged;
        public event Action LimitChanged;

        private readonly PanTiltPosition _position;
        private PanTiltLimit _panTiltlimit;
        private readonly bool _debug;
        private readonly ICommunication _communication;
        private event Action<string> _firmwareVersionReceived;

        private int _sendCount;
        private int _receiveCount;

        public AlturosPanTiltControl(ICommunication communication, bool debug = false)
        {
            if (communication is TcpNetworkCommunication || communication is SerialPortCommunication)
            {
                throw new NotSupportedException("Only upd communication is supported");
            }

            //Add default limits
            this._panTiltlimit = new PanTiltLimit
            {
                PanMin = -100,
                PanMax = 100,
                TiltMin = -60,
                TiltMax = 25
            };

            this._position = new PanTiltPosition(0, 0);

            this._communication = communication;
            this._communication.ReceiveData += PackageReceived;

            this._debug = debug;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this._communication.ReceiveData -= PackageReceived;
        }

        private void PackageReceived(byte[] data)
        {
            if (this._debug)
            {
                var hex = BitConverter.ToString(data);
                Log.Debug($"{nameof(PackageReceived)} - {hex}");
            }

            Interlocked.Increment(ref this._receiveCount);

            var message = Encoding.ASCII.GetString(data);
            if (message.StartsWith("GP", StringComparison.OrdinalIgnoreCase) && message.Length == 14)
            {
                int.TryParse(message.Substring(2, 6), out var tempPan);
                int.TryParse(message.Substring(8, 6), out var tempTilt);

                var pan = tempPan / 100.0;
                var tilt = tempTilt / 100.0;

                this._position.Pan = pan;
                this._position.Tilt = tilt;

                try
                {
                    this.PositionChanged?.Invoke(this._position);
                }
                catch (Exception exception)
                {
                    Log.Error($"{nameof(PackageReceived)}", exception);
                }
            }
            else if (message.StartsWith("ANC", StringComparison.OrdinalIgnoreCase))
            {
                //Announce in application mode
                //Ip
                //Mac
                //Name
                //Serial
            }
            else if (message.StartsWith("GS07", StringComparison.OrdinalIgnoreCase))
            {
                var firmwareVersion = message.Substring(4);
                this._firmwareVersionReceived?.Invoke(firmwareVersion);
            }
            else
            {
                var hex = BitConverter.ToString(data);
                Log.Debug($"{nameof(PackageReceived)} - {hex}");
            }
        }

        private bool Send(string command, string description)
        {
            var commandData = Encoding.ASCII.GetBytes(command);
            return this.Send(commandData, $"{description} {command}");
        }

        private bool Send(byte[] command, string description)
        {
            if (this._debug)
            {
                var hex = BitConverter.ToString(command);
                Log.Debug($"{nameof(Send)} - {hex}");
            }

            Interlocked.Increment(ref this._sendCount);

            try
            {
                return this._communication.Send(command, description);
            }
            catch (ObjectDisposedException exception)
            {
                Log.Debug(nameof(Send), exception);
            }
            catch (Exception exception)
            {
                Log.Error(nameof(Send), exception);
            }

            return false;
        }

        public PanTiltPosition GetPosition()
        {
            //this.Send("GP", "GetPosition");
            return this._position;
        }

        public bool PanAbsolute(double degree)
        {
            //Set a default speed of 30°
            var degreePerSecond = 30;
            this.Send($"SSP{Math.Abs(degreePerSecond * 100):00000}", "PanSpeed");

            return this.Send($"MAP{degree * 100:+00000;-00000;+00000}", "PanAbsolute");
        }

        public bool TiltAbsolute(double degree)
        {
            //Set a default speed of 30°
            var degreePerSecond = 30;
            this.Send($"SST{Math.Abs(degreePerSecond * 100):00000}", "TiltSpeed");

            return this.Send($"MAT{degree * 100:+00000;-00000;+00000}", "TiltAbsolute");
        }

        public bool PanRelative(double degreePerSecond)
        {
            this.Send($"SSP{Math.Abs(degreePerSecond * 100):00000}", "PanSpeed");
            if (degreePerSecond == 0)
            {
                return true;
            }

            if (degreePerSecond > 0)
            {
                return this.Send($"MAP{this._panTiltlimit.PanMax * 100:+00000;-00000;+00000}", "PanRelative");
            }

            return this.Send($"MAP{this._panTiltlimit.PanMin * 100:+00000;-00000;+00000}", "PanRelative");
        }

        public bool TiltRelative(double degreePerSecond)
        {
            this.Send($"SST{Math.Abs(degreePerSecond * 100):00000}", "TiltSpeed");
            if (degreePerSecond == 0)
            {
                return true;
            }

            if (degreePerSecond > 0)
            {
                return this.Send($"MAT{this._panTiltlimit.TiltMax * 100:+00000;-00000;+00000}", "PanRelative");
            }

            return this.Send($"MAT{this._panTiltlimit.TiltMin * 100:+00000;-00000;+00000}", "PanRelative");
        }

        public bool PanTiltAbsolute(double panDegree, double tiltDegree)
        {
            if (this.PanAbsolute(panDegree) && this.TiltAbsolute(tiltDegree))
            {
                return true;
            }

            return false;
        }

        public bool PanTiltRelative(double panDegreePerSecond, double tiltDegreePerSecond)
        {
            if (this.PanRelative(panDegreePerSecond) && this.TiltRelative(tiltDegreePerSecond))
            {
                return true;
            }

            return false;
        }

        public bool StopMoving()
        {
            return this.PanTiltRelative(0, 0);
            //return this.Send($"SSS", "StopMoving");
        }

        public bool ReinitializePtHead()
        {
            return this.Send($"RST", "ReinitializePtHead");
        }

        public bool Start()
        {
            this.Send("SGP0050", "ActivateFeedback");
            return true;
        }

        public bool Stop()
        {
            this.Send("SGP0000", "DisableFeedback");
            return true;
        }

        public PanTiltLimit GetLimits()
        {
            return this._panTiltlimit;
        }

        public bool SetLimits(PanTiltLimit panTiltLimit)
        {
            this._panTiltlimit = panTiltLimit;

            this.LimitChanged?.Invoke();
            return true;
        }

        public async Task<string> GetFirmwareAsync()
        {
            var command = Encoding.ASCII.GetBytes("GS07");

            using (var cannelationTokenSource = new CancellationTokenSource())
            {
                var firmwareVersion = string.Empty;
                void GetFirmware(string data)
                {
                    firmwareVersion = data;
                    cannelationTokenSource.Cancel();
                }

                this._firmwareVersionReceived += GetFirmware;
                this.Send(command, "GetFirmware");
                await Task.Delay(2000, cannelationTokenSource.Token).ContinueWith(tsk => { });
                this._firmwareVersionReceived -= GetFirmware;

                return firmwareVersion;
            }
        }
    }
}
