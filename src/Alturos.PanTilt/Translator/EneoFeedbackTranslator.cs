using Alturos.PanTilt.Manufacturer.Eneo;
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
                sb.Append($"{response.ResponseType},");
            }
            return sb.ToString();
        }
    }
}
