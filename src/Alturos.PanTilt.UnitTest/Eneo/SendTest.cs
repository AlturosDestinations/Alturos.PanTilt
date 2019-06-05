using Alturos.PanTilt.Contract;
using Alturos.PanTilt.UnitTest.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Alturos.PanTilt.UnitTest.Eneo
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

                using (var control = new EneoPanTiltControl(communication, true))
                {
                    control.PanTiltRelative(0, 0);
                    control.PanTiltRelative(1, 0);
                    control.PanTiltRelative(0, 1);
                    control.PanTiltRelative(-1, 0);
                    control.PanTiltRelative(0, -1);
                }

                communication.SendData -= this.SendData;
            }

            Assert.AreEqual(9, this._counter);
        }

        private void SendData(byte[] data, string description)
        {
            Interlocked.Increment(ref this._counter);
        }
    }
}
