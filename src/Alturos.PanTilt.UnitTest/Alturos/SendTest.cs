using Alturos.PanTilt.UnitTest.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Alturos.PanTilt.UnitTest.Alturos
{
    [TestClass]
    public class SendTest
    {
        private int _counter;

        [TestMethod]
        public void SendCommand()
        {
            this._counter = 0;

            using (var communication = new MockCommunication())
            {
                communication.SendData += this.SendData;

                using (var control = new AlturosPanTiltControl(communication, true))
                {
                    // Absolute movement sends one command
                    control.PanAbsolute(-47.17);
                    control.PanAbsolute(47.17);

                    // Relative movement sends two commands
                    control.PanRelative(90.50);
                    control.PanRelative(-90.55);
                    control.TiltRelative(45.05);
                    control.TiltRelative(45.10);
                }

                communication.SendData -= this.SendData;
            }

            Assert.AreEqual(12, this._counter);
        }

        private void SendData(byte[] data, string description)
        {
            Interlocked.Increment(ref this._counter);
        }
    }
}
