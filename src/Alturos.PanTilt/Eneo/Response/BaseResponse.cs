namespace Alturos.PanTilt.Eneo.Response
{
    public class BaseResponse
    {
        public ResponseType ResponseType;
        public bool ChecksumValid;

        public BaseResponse(ResponseType responseType, bool checksumValid)
        {
            this.ResponseType = responseType;
            this.ChecksumValid = checksumValid;
        }
    }
}
