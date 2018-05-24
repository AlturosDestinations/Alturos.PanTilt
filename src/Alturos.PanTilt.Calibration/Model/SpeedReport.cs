namespace Alturos.PanTilt.Calibration.Model
{
    public class SpeedReport
    {
        public int Speed { get; set; }
        public int Distance { get; set; }
        public double Elapsed { get; set; }
        public double DegreePerSecond { get { return this.Distance / (this.Elapsed / 1000); } }
    }
}
