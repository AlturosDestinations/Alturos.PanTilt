namespace Alturos.PanTilt.Contract.Eneo.Response
{
    public class PanLimitResponse : BaseResponse
    {
        public LimitType Type { get; private set; }
        public double Limit { get; private set; }

        public PanLimitResponse(LimitType type, double limit, bool checksumValid) : base(ResponseType.PanLimit, checksumValid)
        {
            this.Type = type;
            this.Limit = limit;
        }
    }
}
