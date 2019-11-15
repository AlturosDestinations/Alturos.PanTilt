using Alturos.PanTilt.UnitTest.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alturos.PanTilt.UnitTest.Alturos
{
    [TestClass]
    public class ReceiveTest
    {
        [TestMethod]
        public void ReceiveDataTest()
        {
            using (var communication = new MockCommunication())
            {
                using (var control = new AlturosPanTiltControl(communication, true))
                {
                    communication.Receive("GP+12445-03589");
                    var position = control.GetPosition();
                    Assert.AreEqual(124.45, position.Pan);
                    Assert.AreEqual(-35.89, position.Tilt);
                }
            }
        }
    }
}
