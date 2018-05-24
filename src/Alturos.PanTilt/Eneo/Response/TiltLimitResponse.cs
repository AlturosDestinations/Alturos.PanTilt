namespace Alturos.PanTilt.Eneo.Response
{
    public class TiltLimitResponse : BaseResponse
    {
        public LimitType Type;
        public double Limit;

        public TiltLimitResponse(LimitType type, double limit, bool checksumValid) : base(ResponseType.TiltLimit, checksumValid)
        {
            this.Type = type;
            this.Limit = limit;
        }
    }
}
