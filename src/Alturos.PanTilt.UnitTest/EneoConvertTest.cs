using Alturos.PanTilt.Manufacturer.Eneo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alturos.PanTilt.UnitTest
{
    [TestClass]
    public class EneoConvertTest
    {
        [TestMethod]
        public void RoundingTestEqual1()
        {
            var positionConverter = new PositionConverter();
            positionConverter.ConvertPositionData(0, out var data1, out var data2);
            positionConverter.ConvertPositionData(6e-16, out var data3, out var data4);

            Assert.AreEqual(data1, data3);
            Assert.AreEqual(data2, data4);
        }

        [TestMethod]
        public void RoundingTestEqual2()
        {
            var positionConverter = new PositionConverter();
            positionConverter.ConvertPositionData(0, out var data1, out var data2);
            positionConverter.ConvertPositionData(0.001, out var data3, out var data4);

            Assert.AreEqual(data1, data3);
            Assert.AreEqual(data2, data4);
        }

        [TestMethod]
        public void RoundingTestNotEqual()
        {
            var positionConverter = new PositionConverter();
            positionConverter.ConvertPositionData(0, out var data1, out var data2);
            positionConverter.ConvertPositionData(0.01, out var data3, out var data4);

            Assert.AreNotEqual(data1, data3);
            Assert.AreNotEqual(data2, data4);
        }
    }
}
