namespace Alturos.PanTilt.Eneo.Response
{
    public class PotentiometerResponse : BaseResponse
    {
        public string Source;
        public double Voltage;

        public PotentiometerResponse(string source, double voltage, bool checksumValid) : base(ResponseType.Potentiometer, checksumValid)
        {
            this.Source = source;
            this.Voltage = voltage;
        }
    }
}
