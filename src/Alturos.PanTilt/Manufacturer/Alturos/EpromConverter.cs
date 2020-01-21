using Alturos.PanTilt.Manufacturer.Alturos.Eprom;
using Alturos.PanTilt.Tools;

namespace Alturos.PanTilt.Manufacturer.Alturos
{
    public class EpromConverter
    {
        public MainConfig Deserialize(byte[] data)
        {
            return ByteConverter.GetObject<MainConfig>(data);
        }

        public byte[] Serialize(MainConfig config)
        {
            return ByteConverter.GetBytes(config);
        }
    }
}
