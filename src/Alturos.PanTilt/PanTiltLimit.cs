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
            return $"PanMin:{Math.Round(this.PanMin, 2)} PanMax:{Math.Round(this.PanMax, 2)} TiltMin:{Math.Round(this.TiltMin, 2)} TiltMax:{Math.Round(this.TiltMax, 2)}";
        }
    }
}
