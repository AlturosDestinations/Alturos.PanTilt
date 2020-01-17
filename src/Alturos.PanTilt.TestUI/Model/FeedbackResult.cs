using System;

namespace Alturos.PanTilt.TestUI.Model
{
    public class FeedbackResult
    {
        public DateTime Timestamp { get; set; }
        public double PanPosition { get; set; }
        public double TiltPosition { get; set; }
        public double PanSpeedCommand { get; set; }
        public double PanSpeedCalculated { get; set; }
        public double PanSpeedDifference { get { return Math.Abs(this.PanSpeedCommand - this.PanSpeedCalculated); } }
        public double TiltSpeedCommand { get; set; }
        public double TiltSpeedCalculated { get; set; }
        public double TiltSpeedDifference { get { return Math.Abs(this.TiltSpeedCommand - this.TiltSpeedCalculated); } }
    }
}
