namespace Alturos.PanTilt.Eneo.Response
{
    public class TiltInfoResponse : BaseResponse
    {
        public double Tilt;

        public TiltInfoResponse(double tilt, bool checksumValid) : base (ResponseType.TiltInfo, checksumValid)
        {
            this.Tilt = tilt;
        }
    }
}
