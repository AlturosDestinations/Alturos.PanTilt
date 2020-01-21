using Alturos.PanTilt.Manufacturer.Alturos;
using Alturos.PanTilt.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alturos.PanTilt.UnitTest.Alturos
{
    [TestClass]
    public class EpromTest
    {
        [TestMethod]
        public void ReadEprom1()
        {
            var epromHex = "0004EE55AAC0A8B8FEFFFFFF00DEADBEEF0002C0A8B82500000000000000000000000000000000000000000000000000000000000000000000000000000000000470743200000000000000320000000040007B00A20362038B2BBFC2880031006203BB03A9A630C30104D007D00700002040000020410000000000004E421405";
            var epromBytes = ByteConverter.HexToByteArray(epromHex);

            var epromConverter = new EpromConverter();
            var config = epromConverter.Deserialize(epromBytes);

            var epromBytesNew = epromConverter.Serialize(config);
            var epromHexNew = ByteConverter.ByteArrayToHex(epromBytesNew);

            Assert.AreEqual(epromHexNew, epromHex);
        }
    }
}
