namespace Alturos.PanTilt.Contract.Eneo.Response
{
    public class TiltLimitResponse : BaseResponse
    {
        public LimitType Type { get; private set; }
        public double Limit { get; private set; }

        public TiltLimitResponse(LimitType type, double limit, bool checksumValid) : base(ResponseType.TiltLimit, checksumValid)
        {
            this.Type = type;
            this.Limit = limit;
        }
    }
}
