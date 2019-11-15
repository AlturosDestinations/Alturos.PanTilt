namespace Alturos.PanTilt.Eneo.Response
{
    public class LimitActiveResponse : BaseResponse
    {
        public LimitActiveResponse(bool checksumValid) : base(ResponseType.LimitActive, checksumValid)
        {
        }
    }
}
