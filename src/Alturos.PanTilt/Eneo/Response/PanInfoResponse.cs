namespace Alturos.PanTilt.Eneo.Response
{
    public class PanInfoResponse : BaseResponse
    {
        public double Pan { get; private set; }

        public PanInfoResponse(double pan, bool checksumValid) : base(ResponseType.PanInfo, checksumValid)
        {
            this.Pan = pan;
        }
    }
}
