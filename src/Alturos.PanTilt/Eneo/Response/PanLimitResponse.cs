namespace Alturos.PanTilt.Eneo.Response
{
    public class PanLimitResponse : BaseResponse
    {
        public LimitType Type;
        public double Limit;

        public PanLimitResponse(LimitType type, double limit, bool checksumValid) : base(ResponseType.PanLimit, checksumValid)
        {
            this.Type = type;
            this.Limit = limit;
        }
    }
}
