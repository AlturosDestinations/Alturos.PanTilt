using System;

namespace Alturos.PanTilt.TestUI.Model
{
    public class PositionCompare
    {
        public int MoveTime { get; set; }
        public double DegreePerSecond { get; set; }
        public double ActualPosition { get; set; }
        public double TargetPosition { get; set; }
        public double DifferencePerSecond
        {
            get
            {
                return Math.Abs(Math.Round(this.TargetPosition - this.ActualPosition, 2)) / (this.MoveTime / 1000);
            }
        }
    }
}
