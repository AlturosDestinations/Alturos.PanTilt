using Alturos.PanTilt.Calibration.Model;
using Alturos.PanTilt.Communication;
using Alturos.PanTilt.Tools;
using System;
using System.Threading;

namespace Alturos.PanTilt.Calibration
{
    public class SpeedTestLogic : IDisposable
    {
        private readonly AxisType _axisType;
        private readonly IPanTiltControl _panTiltControl;
        private readonly IPositionChecker _positionChecker;
        private double _lastPosition;
        public double LastPosition { get { return this._lastPosition; } }

        public SpeedTestLogic(ICommunication communication, PanTiltControlType panTiltControlType, AxisType axisType)
        {
            this._axisType = axisType;
            if (panTiltControlType == PanTiltControlType.Eneo)
            {
                this._panTiltControl = new EneoPanTiltControl(communication);
            }
            else
            {
                this._panTiltControl = new AlturosPanTiltControl(communication);
            }
            this._panTiltControl.PositionChanged += OnPositionChanged;
            this._positionChecker = new PositionChecker(this._panTiltControl);

            this._panTiltControl.Start();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this._panTiltControl.PositionChanged -= OnPositionChanged;
            this._panTiltControl?.StopMoving();
            this._panTiltControl?.Stop();
            this._panTiltControl?.Dispose();
        }

        private void OnPositionChanged(PanTiltPosition pt)
        {
            switch (this._axisType)
            {
                case AxisType.Pan:
                    this._lastPosition = pt.Pan;
                    break;
                case AxisType.Tilt:
                    this._lastPosition = pt.Tilt;
                    break;
            }
        }

        public void GoToStartPosition(int position)
        {
            PanTiltPosition panTiltPosition = null;

            switch (this._axisType)
            {
                case AxisType.Pan:
                    panTiltPosition = new PanTiltPosition(position, 0);
                    this._panTiltControl.PanTiltAbsolute(position, 0);
                    break;
                case AxisType.Tilt:
                    panTiltPosition = new PanTiltPosition(0, position);
                    this._panTiltControl.PanTiltAbsolute(0, position);
                    break;
            }

            while (!this._positionChecker.ComparePosition(panTiltPosition, tolerance: 0.2))
            {
                Thread.Sleep(100);
            }
        }

        public void Move(double degreePerSecond, int durationMilliseconds)
        {
            switch (this._axisType)
            {
                case AxisType.Pan:
                    this._panTiltControl.PanRelative(degreePerSecond);
                    break;
                case AxisType.Tilt:
                    this._panTiltControl.TiltRelative(degreePerSecond);
                    break;
            }
          
            Thread.Sleep(durationMilliseconds);
            this._panTiltControl.StopMoving();
        }
    }
}
