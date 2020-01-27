using Alturos.PanTilt.Manufacturer.Eneo;
using Alturos.PanTilt.Manufacturer.Eneo.Response;
using System.Text;

namespace Alturos.PanTilt.Translator
{
    public class EneoFeedbackTranslator : IFeedbackTranslator
    {
        private readonly FeedbackHandler _feedbackHandler;

        public EneoFeedbackTranslator()
        {
            this._feedbackHandler = new FeedbackHandler();
        }

        public string Translate(byte[] data)
        {
            var sb = new StringBuilder();
            var responses = this._feedbackHandler.HandleResponse(data);
            foreach (var response in responses)
            {
                if (response is TiltInfoResponse tiltInfoResponse)
                {
                    sb.Append($"{response.ResponseType} {tiltInfoResponse.Tilt},");
                    continue;
                }

                if (response is PanInfoResponse panInfoResponse)
                {
                    sb.Append($"{response.ResponseType} {panInfoResponse.Pan},");
                    continue;
                }

                sb.Append($"{response.ResponseType},");
            }
            return sb.ToString();
        }
    }
}
