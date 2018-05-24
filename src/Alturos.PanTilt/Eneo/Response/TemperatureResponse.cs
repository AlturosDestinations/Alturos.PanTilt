namespace Alturos.PanTilt.Eneo.Response
{
    public class TemperatureResponse : BaseResponse
    {
        public double Temperature;

        public TemperatureResponse(double temperature, bool checksumValid) : base(ResponseType.Temperature, checksumValid)
        {
            this.Temperature = temperature;
        }
    }
}
