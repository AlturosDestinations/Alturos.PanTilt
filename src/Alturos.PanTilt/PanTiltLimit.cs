using System;

namespace Alturos.PanTilt
{
    public class PanTiltLimit
    {
        public double PanMin { get; set; }
        public double PanMax { get; set; }
        public double TiltMin { get; set; }
        public double TiltMax { get; set; }

        public override string ToString()
        {
            return $"{Math.Round(PanMin, 0)}_{Math.Round(PanMax, 0)}_{Math.Round(TiltMin, 0)}_{Math.Round(TiltMax, 0)}";
        }
    }
}
