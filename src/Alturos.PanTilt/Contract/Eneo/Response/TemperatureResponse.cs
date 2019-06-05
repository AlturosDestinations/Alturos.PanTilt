namespace Alturos.PanTilt.Contract.Eneo.Response
{
    public class TemperatureResponse : BaseResponse
    {
        public double Temperature { get; private set; }

        public TemperatureResponse(double temperature, bool checksumValid) : base(ResponseType.Temperature, checksumValid)
        {
            this.Temperature = temperature;
        }
    }
}
