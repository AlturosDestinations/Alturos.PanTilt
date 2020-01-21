using Alturos.PanTilt.Communication;
using Alturos.PanTilt.Manufacturer.Alturos;
using Alturos.PanTilt.Manufacturer.Alturos.Eprom;
using Alturos.PanTilt.Tools;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
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

        private readonly ICommunication _communication;

        private readonly bool _debug;
        private readonly PanTiltPosition _position;
        private PanTiltLimit _panTiltlimit;
        private CancellationTokenSource _cancellationTokenSource;

        private int _sendCount;
        private int _receiveCount;
        private DateTime _lastPositionReceiveDate;

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
                if (!int.TryParse(message.Substring(2, 6), out var tempPan))
                {
                    Log.Warn($"{nameof(PackageReceived)} - Cannot parse pan {message}");
                }

                if (!int.TryParse(message.Substring(8, 6), out var tempTilt))
                {
                    Log.Warn($"{nameof(PackageReceived)} - Cannot parse tilt {message}");
                }

                var pan = tempPan / 100.0;
                var tilt = tempTilt / 100.0;

                this._position.Pan = pan;
                this._position.Tilt = tilt;
                this._lastPositionReceiveDate = DateTime.Now;

                try
                {
                    var subscribers = this.PositionChanged?.GetInvocationList();
                    if (subscribers != null)
                    {
                        foreach (var subscriber in subscribers)
                        {
                            Task.Run(() => subscriber.DynamicInvoke(this._position));
                        }
                    }
                }
                catch (Exception exception)
                {
                    Log.Error($"{nameof(PackageReceived)}", exception);
                }
            }
            else if (message.StartsWith("AN", StringComparison.OrdinalIgnoreCase) && message.Length == 37)
            {
                var announce = message.Substring(2);
                var ip = this.GetIpAddressFromHex(announce.Substring(0, 8));
                var mac = announce.Substring(8, 12);
                //Name
                //Serial
            }
            else
            {
                var hex = BitConverter.ToString(data);
                Log.Debug($"{nameof(PackageReceived)} - {hex}");
            }
        }

        private string GetIpAddressFromHex(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return null;
            }

            return new IPAddress((BitConverter.IsLittleEndian ? IPAddress.HostToNetworkOrder(Convert.ToInt32(hex, 16)) : Convert.ToInt32(hex, 16))).ToString();
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

        public bool Start()
        {
            this._cancellationTokenSource?.Cancel();
            this._cancellationTokenSource?.Dispose();
            this._cancellationTokenSource = new CancellationTokenSource();

            _ = Task.Run(async () => { await this.FeedbackMonitoringAsync(); }, this._cancellationTokenSource.Token).ContinueWith(t => { });
            return true;
        }

        public bool Stop()
        {
            this._cancellationTokenSource?.Cancel();
            this._cancellationTokenSource?.Dispose();

            return this.DisableFeedback();
        }

        private async Task FeedbackMonitoringAsync()
        {
            if (this.ActivateFeedback())
            {
                Log.Warn($"{nameof(FeedbackMonitoringAsync)} - Cannot activate feedback");
            }

            while (!this._cancellationTokenSource.IsCancellationRequested)
            {
                if (this._lastPositionReceiveDate > DateTime.Now.AddSeconds(-10))
                {
                    await Task.Delay(10000, this._cancellationTokenSource.Token).ContinueWith(t => { });
                    continue;
                }

                this.ActivateFeedback();
                await Task.Delay(1000, this._cancellationTokenSource.Token).ContinueWith(t => { });
            }
        }

        private bool ActivateFeedback()
        {
            return this.Send("SGP0050", "ActivateFeedback");
        }

        private bool DisableFeedback()
        {
            return this.Send("SGP0000", "DisableFeedback");
        }

        public PanTiltPosition GetPosition()
        {
            return this._position;
        }

        public bool PanAbsolute(double degree)
        {
            #region Check limits

            if (degree > 0 && degree > this._panTiltlimit.PanMax)
            {
                degree = this._panTiltlimit.PanMax;
            }
            else if (degree < 0 && degree < this._panTiltlimit.PanMin)
            {
                degree = this._panTiltlimit.PanMin;
            }

            #endregion

            //Set a default speed of 30°
            var degreePerSecond = 30;
            this.Send($"SSP{Math.Abs(degreePerSecond * 100):00000}", "PanSpeed");

            return this.Send($"MAP{degree * 100:+00000;-00000;+00000}", "PanAbsolute");
        }

        public bool TiltAbsolute(double degree)
        {
            #region Check limits

            if (degree > 0 && degree > this._panTiltlimit.TiltMax)
            {
                degree = this._panTiltlimit.TiltMax;
            }
            else if (degree < 0 && degree < this._panTiltlimit.TiltMin)
            {
                degree = this._panTiltlimit.TiltMin;
            }

            #endregion

            //Set a default speed of 30°
            var degreePerSecond = 30;
            this.Send($"SST{Math.Abs(degreePerSecond * 100):00000}", "TiltSpeed");

            return this.Send($"MAT{degree * 100:+00000;-00000;+00000}", "TiltAbsolute");
        }

        public bool PanTiltAbsolute(double panDegree, double tiltDegree)
        {
            if (this.PanAbsolute(panDegree) && this.TiltAbsolute(tiltDegree))
            {
                return true;
            }

            return false;
        }

        public bool PanTiltAbsolute(PanTiltPosition position)
        {
            return this.PanTiltAbsolute(position.Pan, position.Tilt);
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

        #region Get Statistic Data

        private async Task<string> GetStatisticDataAsync(string command, string commandDescription)
        {
            try
            {
                var commandData = Encoding.ASCII.GetBytes(command);

                using (var cannelationTokenSource = new CancellationTokenSource())
                {
                    var value = string.Empty;

                    this._communication.ReceiveData += ProcessData;

                    void ProcessData(byte[] data)
                    {
                        var message = Encoding.ASCII.GetString(data);
                        if (message.StartsWith(command, StringComparison.OrdinalIgnoreCase))
                        {
                            value = message.Substring(command.Length);
                            cannelationTokenSource.Cancel();
                        }
                    }

                    this.Send(commandData, commandDescription);
                    await Task.Delay(2000, cannelationTokenSource.Token).ContinueWith(tsk => { });

                    this._communication.ReceiveData -= ProcessData;

                    return value;
                }
            }
            catch (Exception exception)
            {
                Log.Error(nameof(GetStatisticDataAsync), exception);
                return null;
            }
        }

        private async Task<string> GetEpromDataAsync(string command, string commandDescription)
        {
            try
            {
                var commandData = Encoding.ASCII.GetBytes(command);

                using (var cannelationTokenSource = new CancellationTokenSource())
                {
                    var eprom = new StringBuilder();
                    var list = new List<string>();
                    this._communication.ReceiveData += ProcessData;

                    void ProcessData(byte[] data)
                    {
                        var message = Encoding.ASCII.GetString(data);
                        if (message.StartsWith(command, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(message);

                            var packageInfoLength = 2;
                            var index = message.Substring(command.Length, packageInfoLength);
                            eprom.Append(message.Substring(command.Length + packageInfoLength));

                            if (index == "07")
                            {
                                cannelationTokenSource.Cancel();
                            }
                        }
                    }

                    this.Send(commandData, commandDescription);
                    await Task.Delay(2000, cannelationTokenSource.Token).ContinueWith(tsk => { });

                    this._communication.ReceiveData -= ProcessData;

                    return eprom.ToString();
                }
            }
            catch (Exception exception)
            {
                Log.Error(nameof(GetEpromDataAsync), exception);
                return null;
            }
        }

        private async Task<double> GetDoubleStatisticDataAsync(string command, string commandDescription)
        {
            var value = await this.GetStatisticDataAsync(command, commandDescription);
            if (!int.TryParse(value, out var tempValue))
            {
                Log.Warn($"{nameof(GetDoubleStatisticDataAsync)} - Cannot parse response for {command} {value}");
            }

            return tempValue / 100.0;
        }

        public async Task<string> GetFirmwareAsync()
        {
            return await this.GetStatisticDataAsync("GS07", "GetFirmwareVersion");
        }

        public async Task<double> GetTemperatureAsync()
        {
            return await this.GetDoubleStatisticDataAsync("GS01", "GetTemperature");
        }

        public async Task<double> GetHumidityAsync()
        {
            return await this.GetDoubleStatisticDataAsync("GS02", "GetHumidity");
        }

        public async Task<double> GetPanHalSensorAsync()
        {
            return await this.GetDoubleStatisticDataAsync("GS03", "GetPanHalSensor");
        }

        public async Task<double> GetTiltHalSensorAsync()
        {
            return await this.GetDoubleStatisticDataAsync("GS04", "GetTiltHalSensor");
        }

        public async Task<string> GetPanInitialErrorAsync()
        {
            return await this.GetStatisticDataAsync("GS05", "GetPanInitialError");
        }

        public async Task<string> GetTiltInitialErrorAsync()
        {
            return await this.GetStatisticDataAsync("GS06", "GetTiltInitialError");
        }

        public async Task<MainConfig> GetConfigAsync()
        {
            var hexEpromData = await this.GetEpromDataAsync("GE", "GetEpromData");
            if (string.IsNullOrEmpty(hexEpromData))
            {
                return default(MainConfig);
            }

            var epromData = ByteConverter.HexToByteArray(hexEpromData);

            var converter = new EpromConverter();
            return converter.Deserialize(epromData);
        }

        public async Task<bool> SetConfigAsync(MainConfig mainConfig)
        {
            var converter = new EpromConverter();
            var configBytes = converter.Serialize(mainConfig);
            for (var i = 0; i < configBytes.Length; i++)
            {
                //this.Send($"SE{i:X2}{configBytes[i]:X2}", "SetEpromData");
                Log.Debug($"SE{i:X2}{configBytes[i]:X2}");
            }

            return await Task.FromResult(true);
        }

        #endregion

        public PanTiltInfo GetPanTiltInfo()
        {
            return new PanTiltInfo
            {
                PanSpeedMax = 100,
                TiltSpeedMax = 65
            };
        }
    }
}
