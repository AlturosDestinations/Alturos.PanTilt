using Alturos.PanTilt.Contract;
using Alturos.PanTilt.UnitTest.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Alturos.PanTilt.UnitTest.Eneo
{
    [TestClass]
    public class ProtocolTest
    {
        private readonly Dictionary<string, string> _sendHistory = new Dictionary<string, string>();
        private bool _successful = true;

        [TestMethod]
        public void CheckEneoProtocoll()
        {
            using (var communication = new MockCommunication())
            {
                communication.SendData += this.SendData;

                using (var control = new EneoPanTiltControl(communication, true))
                {
                    for (var pan = -180.0M; pan < 180; pan += 0.05M)
                    {
                        control.PanAbsolute(Convert.ToDouble(pan));
                    }

                    for (var tilt = -90.0M; tilt < 90; tilt += 0.05M)
                    {
                        control.TiltAbsolute(Convert.ToDouble(tilt));
                    }
                }

                communication.SendData -= this.SendData;
            }

            Assert.IsTrue(this._successful);
        }

        private void SendData(byte[] data, string description)
        {
            var hex = BitConverter.ToString(data);

            if (this._sendHistory.ContainsKey(hex))
            {
                var duplicate = this._sendHistory[hex];
                Debug.WriteLine($"Duplicate detect {description} - {duplicate} ({hex})");
                this._successful = false;

                return;
            }

            this._sendHistory.Add(hex, description);
        }
    }
}
