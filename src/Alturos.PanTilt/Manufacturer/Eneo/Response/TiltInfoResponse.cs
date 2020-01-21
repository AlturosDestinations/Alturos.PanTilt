namespace Alturos.PanTilt.Manufacturer.Eneo.Response
{
    public class TiltInfoResponse : BaseResponse
    {
        public double Tilt { get; private set; }

        public TiltInfoResponse(double tilt, bool checksumValid) : base (ResponseType.TiltInfo, checksumValid)
        {
            this.Tilt = tilt;
        }
    }
}
