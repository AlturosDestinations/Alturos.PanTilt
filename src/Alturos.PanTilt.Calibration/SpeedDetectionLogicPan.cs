using Alturos.PanTilt.Communication;
using Alturos.PanTilt.Tools;
using System;
using System.Threading;

namespace Alturos.PanTilt.Calibration
{
    public class SpeedDetectionLogicPan : IDisposable
    {
        private readonly ICommunication _communication;
        private readonly IPanTiltControl _panTiltControl;
        private readonly IPositionChecker _positionChecker;
        private int _stop;
        private bool _active = false;
        private readonly AutoResetEvent _resetEvent = new AutoResetEvent(false);

        public SpeedDetectionLogicPan(ICommunication communication, PanTiltControlType panTiltControlType)
        {
            this._communication = communication;
            if (panTiltControlType == PanTiltControlType.Eneo)
            {
                this._panTiltControl = new EneoPanTiltControl(this._communication);
            }
            else
            {
                this._panTiltControl = new AlturosPanTiltControl(this._communication);
            }
            
            this._panTiltControl.PositionChanged += OnPositionChanged;
            this._positionChecker = new PositionChecker(this._panTiltControl);

            this._panTiltControl.Start();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._panTiltControl != null)
            {
                this._panTiltControl.PositionChanged -= OnPositionChanged;
            }

            this._panTiltControl?.StopMoving();
            this._panTiltControl?.Stop();
            this._panTiltControl?.Dispose();
            this._communication?.Dispose();
        }

        public void GoToStartPosition(int position)
        {
            this._active = false;
            this._panTiltControl.PanTiltAbsolute(position, 0);
            while (!this._positionChecker.ComparePosition(new PanTiltPosition(position, 0), retry: 10))
            {
                Console.WriteLine($"Position is invalid {position}/{this._panTiltControl.GetPosition().Pan}");
                Thread.Sleep(500);
            }
        }

        public void Relative(int speed)
        {
            this._panTiltControl.PanRelative(speed);
        }

        public void Start(int speed, int stop)
        {
            this._stop = stop;
            this._panTiltControl.PanRelative(speed);
            this._active = true;
            this._resetEvent.WaitOne(60000);
            this._panTiltControl.StopMoving();
        }

        private void OnPositionChanged(PanTiltPosition pt)
        {
            if (this._active && pt.Pan > this._stop)
            {
                this._resetEvent.Set();
            }
        }
    }
}
