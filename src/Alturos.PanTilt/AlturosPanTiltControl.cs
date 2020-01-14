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

        private readonly ICommunication _communication;

        private readonly bool _debug;
        private readonly PanTiltPosition _position;
        private PanTiltLimit _panTiltlimit;
        private event Action<string> _firmwareVersionReceived;
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
