using log4net;
using System;
using System.Text;
using System.Threading;

namespace Alturos.PanTilt.Contract
{
    public class AlturosPanTiltControl : IPanTiltControl
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AlturosPanTiltControl));

        public event Action<PanTiltPosition> PositionChanged;
        public event Action LimitChanged;
        public event Action LimitOverrun;

        private PanTiltPosition _position;
        private readonly bool _debug;
        private readonly ICommunication _communication;

        private int _sendCount;
        private int _receiveCount;

        public AlturosPanTiltControl(ICommunication communication, bool debug = false)
        {
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
            if (message.Length == 14)
            {
                int.TryParse(message.Substring(2, 6), out var tempPan);
                int.TryParse(message.Substring(8, 6), out var tempTilt);

                this._position = new PanTiltPosition(tempPan / 100.0, tempTilt / 100.0);
                this.PositionChanged?.Invoke(this._position);
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

        public PanTiltLimit GetLimits()
        {
            return new PanTiltLimit();
        }

        public PanTiltPosition GetPosition()
        {
            //this.Send("GP", "GetPosition");
            return this._position;
        }

        public bool PanAbsolute(double pan)
        {
            return this.Send($"MAP{pan * 100:+00000;-00000;+00000}", "PanAbsolute");
        }

        public bool TiltAbsolute(double tilt)
        {
            return this.Send($"MAT{tilt * 100:+00000;-00000;+00000}", "TiltAbsolute");
        }

        public bool PanRelative(double panSpeed)
        {
            this.Send($"SSP{panSpeed * 100:00000}", "PanSpeed");
            if (panSpeed > 0)
            {
                return this.Send($"MRP{180 * 100:+00000;-00000;+00000}", "PanRelative");
            }

            return this.Send($"MRP{-180 * 100:+00000;-00000;+00000}", "PanRelative");
        }

        public bool TiltRelative(double tiltSpeed)
        {
            this.Send($"SST{tiltSpeed * 100:00000}", "TiltSpeed");
            if (tiltSpeed > 0)
            {
                return this.Send($"MRT{180 * 100:+00000;-00000;+00000}", "PanRelative");
            }

            return this.Send($"MRT{-180 * 100:+00000;-00000;+00000}", "PanRelative");
        }

        public bool PanTiltAbsolute(double pan, double tilt)
        {
            if (this.PanAbsolute(pan) && this.TiltAbsolute(tilt))
            {
                return true;
            }

            return false;
        }

        public bool PanTiltRelative(double panSpeed, double tiltSpeed)
        {
            if (this.PanRelative(panSpeed) && this.TiltRelative(tiltSpeed))
            {
                return true;
            }

            return false;
        }

        public bool StopMoving()
        {
            return this.PanTiltRelative(0, 0);
        }

        public bool ReinitializePosition()
        {
            return this.Send($"RST", "ReinitializePosition");
        }

        public bool SetLimits(PanTiltLimit panTiltLimit)
        {
            return false;
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
    }
}
