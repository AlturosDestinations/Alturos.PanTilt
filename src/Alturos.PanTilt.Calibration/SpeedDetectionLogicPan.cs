﻿using Alturos.PanTilt.Eneo;
using System;
using System.Threading;

namespace Alturos.PanTilt.Calibration
{
    public class SpeedDetectionLogicPan : IDisposable
    {
        private ICommunication _communication;
        private IPanTiltControl _panTiltControl;
        private int _stop;
        private bool _active = false;
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);

        public SpeedDetectionLogicPan(ICommunication communication)
        {
            this._communication = communication;
            this._panTiltControl = new EneoPanTiltControl(this._communication);
            this._panTiltControl.PositionChanged += OnPositionChanged;
            this._panTiltControl.Start();
        }

        public void Dispose()
        {
            this._panTiltControl?.StopMoving();
            this._panTiltControl?.Stop();
            this._panTiltControl?.Dispose();
            this._communication?.Dispose();
        }

        public void GoToStartPosition(int position)
        {
            this._active = false;
            this._panTiltControl.PanTiltAbsolute(position, 0);
            while (!this._panTiltControl.ComparePosition(new PanTiltPosition(position, 0), retry: 10))
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
