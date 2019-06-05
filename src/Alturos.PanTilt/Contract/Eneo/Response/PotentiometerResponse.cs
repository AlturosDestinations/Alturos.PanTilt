namespace Alturos.PanTilt.Contract.Eneo.Response
{
    public class PotentiometerResponse : BaseResponse
    {
        public string Source { get; private set; }
        public double Voltage { get; private set; }

        public PotentiometerResponse(string source, double voltage, bool checksumValid) : base(ResponseType.Potentiometer, checksumValid)
        {
            this.Source = source;
            this.Voltage = voltage;
        }
    }
}
