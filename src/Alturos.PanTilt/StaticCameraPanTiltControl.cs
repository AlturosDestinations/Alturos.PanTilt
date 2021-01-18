using System;
using System.Threading.Tasks;

namespace Alturos.PanTilt
{
    public class StaticCameraPanTiltControl : IPanTiltControl
    {
        public event Action<PanTiltPosition> PositionChanged;
        public event Action NoPositionFeedbackReceived;
        public event Action LimitChanged;

        public void Dispose()
        {

        }

        public PanTiltLimit GetLimits()
        {
            return new PanTiltLimit();
        }

        public PanTiltInfo GetPanTiltInfo()
        {
            return new PanTiltInfo();
        }

        public PanTiltPosition GetPosition()
        {
            return new PanTiltPosition();
        }

        public bool PanAbsolute(double degree)
        {
            return true;
        }

        public bool PanRelative(double degreePerSecond)
        {
            return true;
        }

        public bool PanTiltAbsolute(double panDegree, double tiltDegree)
        {
            return true;
        }

        public bool PanTiltAbsolute(PanTiltPosition position)
        {
            return true;
        }

        public bool PanTiltRelative(double panDegreePerSecond, double tiltDegreePerSecond)
        {
            return true;
        }

        public bool ReinitializePtHead()
        {
            return true;
        }

        public bool SetLimits(PanTiltLimit panTiltLimit)
        {
            //this.LimitChanged?.Invoke();
            return true;
        }

        public bool Start()
        {
            return true;
        }

        public bool Stop()
        {
            return true;
        }

        public bool StopMoving()
        {
            return true;
        }

        public bool TiltAbsolute(double degree)
        {
            return true;
        }

        public bool TiltRelative(double degreePerSecond)
        {
            return true;
        }
        public Task<double> GetTemperatureAsync()
        {
            return Task.FromResult(0.00);
        }
        public Task<double> GetHumidityAsync()
        {
            return Task.FromResult(0.00);
        }
        public Task<string> GetHeaterActiceAsync()
        {
            return Task.FromResult("-1");
        }
        public Task<string> GetEnvironmentErrorAsync()
        {
            return Task.FromResult("-1");
        }
    }
}
