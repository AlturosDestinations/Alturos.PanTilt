namespace Alturos.PanTilt.Eneo.Response
{
    public class BaseResponse
    {
        public ResponseType ResponseType { get; private set; }
        public bool ChecksumValid { get; private set; }

        public BaseResponse(ResponseType responseType, bool checksumValid)
        {
            this.ResponseType = responseType;
            this.ChecksumValid = checksumValid;
        }
    }
}
