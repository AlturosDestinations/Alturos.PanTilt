using System;

namespace Alturos.PanTilt.Calibration.Model
{
    public class SpeedReport
    {
        private double _elapsed;

        public double Speed { get; set; }
        public int Distance { get; set; }
        public double Elapsed
        {
            get
            {
                return Math.Round(this._elapsed, 2);
            }
            set
            {
                this._elapsed = value;
            }
        }

        public double DegreePerSecond
        {
            get
            {
                return Math.Round(this.Distance / (this.Elapsed / 1000), 3);
            }
        }


        public double Diff
        {
            get
            {
                return Math.Round(Math.Abs(this.Speed - this.DegreePerSecond), 3);
            }
        }
    }
}
