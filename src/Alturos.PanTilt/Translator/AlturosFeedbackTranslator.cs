using System.Text;

namespace Alturos.PanTilt.Translator
{
    public class AlturosFeedbackTranslator : IFeedbackTranslator
    {
        public string Translate(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }
    }
}
