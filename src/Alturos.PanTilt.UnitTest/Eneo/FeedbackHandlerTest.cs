using Alturos.PanTilt.Manufacturer.Eneo;
using Alturos.PanTilt.Manufacturer.Eneo.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alturos.PanTilt.UnitTest.Eneo
{
    [TestClass]
    public class FeedbackHandlerTest
    {
        [TestMethod]
        public void ReceivePanTiltPosition()
        {
            var handler = new FeedbackHandler();
            var items = handler.HandleResponse(new byte[] { 0xFF, 0x01, 0xCA, 0x0A, 0x1B, 0x5A, 0x4A, 0xFF, 0x01, 0xCB, 0x0B, 0x01, 0x0F, 0xE7 });
            Assert.AreEqual(2, items.Count);
            Assert.AreEqual(ResponseType.TiltInfo, items[0].ResponseType);
            Assert.AreEqual(70.02, ((TiltInfoResponse)items[0]).Tilt);
            Assert.AreEqual(ResponseType.PanInfo, items[1].ResponseType);
            Assert.AreEqual(-2.71, ((PanInfoResponse)items[1]).Pan);
        }

        [TestMethod]
        public void ReceivePanTiltPositionTwoPartsWithTheFirstByteOfTheSecondPackageOnTheFirstPackage()
        {
            var handler = new FeedbackHandler();
            var items = handler.HandleResponse(new byte[] { 0xFF, 0x01, 0xCA, 0x0A, 0x1B, 0x5A, 0x4A, 0xFF });
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(ResponseType.TiltInfo, items[0].ResponseType);
            Assert.AreEqual(70.02, ((TiltInfoResponse)items[0]).Tilt);

            items = handler.HandleResponse(new byte[] { 0x01, 0xCB, 0x0B, 0x01, 0x0F, 0xE7 });

            Assert.AreEqual(ResponseType.PanInfo, items[0].ResponseType);
            Assert.AreEqual(-2.71, ((PanInfoResponse)items[0]).Pan);
        }

        [TestMethod]
        public void ReceiveFragmentedPackage()
        {
            var handler = new FeedbackHandler();
            var items = handler.HandleResponse(new byte[] { 0xFF, 0x01, 0xCA });
            Assert.AreEqual(0, items.Count);
            items = handler.HandleResponse(new byte[] { 0x0A, 0x1B });
            Assert.AreEqual(0, items.Count);
            items = handler.HandleResponse(new byte[] { 0x5A });
            Assert.AreEqual(0, items.Count);
            items = handler.HandleResponse(new byte[] { 0x4A });

            Assert.AreEqual(ResponseType.TiltInfo, items[0].ResponseType);
            Assert.AreEqual(70.02, ((TiltInfoResponse)items[0]).Tilt);
        }

        [TestMethod]
        public void ReceivePanLimitMaxPosition()
        {
            var handler = new FeedbackHandler();
            var items = handler.HandleResponse(new byte[] { 0xFF, 0x01, 0xAB, 0x0A, 0x46, 0x31, 0x2D });
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(ResponseType.PanLimit, items[0].ResponseType);
            Assert.AreEqual(LimitType.Max, ((PanLimitResponse)items[0]).Type);
            Assert.AreEqual(179.69, ((PanLimitResponse)items[0]).Limit);
        }

        [TestMethod]
        public void ReceiveTiltLimitMinPosition()
        {
            var handler = new FeedbackHandler();
            var items = handler.HandleResponse(new byte[] { 0xFF, 0x01, 0xAC, 0x0B, 0x23, 0xA2, 0x7D });
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(ResponseType.TiltLimit, items[0].ResponseType);
            Assert.AreEqual(LimitType.Min, ((TiltLimitResponse)items[0]).Type);
            Assert.AreEqual(-91.22, ((TiltLimitResponse)items[0]).Limit);
        }

        [TestMethod]
        public void ReceivePanLimitMin()
        {
            var handler = new FeedbackHandler();
            var items = handler.HandleResponse(new byte[] { 0xFF, 0x01, 0xAD, 0x0A, 0x23, 0x9B, 0x76 });
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(ResponseType.TiltLimit, items[0].ResponseType);
            Assert.AreEqual(LimitType.Max, ((TiltLimitResponse)items[0]).Type);
            Assert.AreEqual(91.15, ((TiltLimitResponse)items[0]).Limit);
        }

        [TestMethod]
        public void ReceiveInvalidPackageWrongFirstByte()
        {
            var handler = new FeedbackHandler();
            var items = handler.HandleResponse(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Assert.AreEqual(0, items.Count);
        }

        [TestMethod]
        public void ReceiveInvalidPackageToShort()
        {
            var handler = new FeedbackHandler();
            var items = handler.HandleResponse(new byte[] { 0x00 });
            Assert.AreEqual(0, items.Count);
        }

        [TestMethod]
        public void ReceiveInvalidPackageToLarge()
        {
            var handler = new FeedbackHandler();
            var items = handler.HandleResponse(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Assert.AreEqual(0, items.Count);
        }

        [TestMethod]
        public void ReceiveFirstCompleteWithAIncomplete()
        {
            var handler = new FeedbackHandler();
            var items = handler.HandleResponse(new byte[] { 0xFF, 0x01, 0xAB, 0x0A, 0x46, 0x31, 0x2D, 0xFF, 0x01 });
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(ResponseType.PanLimit, items[0].ResponseType);
            Assert.AreEqual(LimitType.Max, ((PanLimitResponse)items[0]).Type);
            Assert.AreEqual(179.69, ((PanLimitResponse)items[0]).Limit);
        }

        [TestMethod]
        public void ReceiveTwoComplete()
        {
            var handler = new FeedbackHandler();
            var items = handler.HandleResponse(new byte[] { 0xFF, 0x01, 0xAB, 0x0A, 0x46, 0x31, 0x2D, 0xFF, 0x01, 0xAB, 0x0A, 0x46, 0x31, 0x2D });
            Assert.AreEqual(2, items.Count);

            Assert.AreEqual(ResponseType.PanLimit, items[0].ResponseType);
            Assert.AreEqual(LimitType.Max, ((PanLimitResponse)items[0]).Type);
            Assert.AreEqual(179.69, ((PanLimitResponse)items[0]).Limit);

            Assert.AreEqual(ResponseType.PanLimit, items[1].ResponseType);
            Assert.AreEqual(LimitType.Max, ((PanLimitResponse)items[1]).Type);
            Assert.AreEqual(179.69, ((PanLimitResponse)items[1]).Limit);
        }

        [TestMethod]
        public void ParseLimitOverrunResponseBoth()
        {
            var handler = new FeedbackHandler();
            var limitOverrunType = handler.GetLimitOverrunType(0x4C, 0x50, 0x54);
            Assert.AreEqual(LimitOverrunType.Both, limitOverrunType);
        }

        [TestMethod]
        public void ParseLimitOverrunResponsePan()
        {
            var handler = new FeedbackHandler();
            var limitOverrunType = handler.GetLimitOverrunType(0x4C, 0x50, 0x50);
            Assert.AreEqual(LimitOverrunType.Pan, limitOverrunType);
        }

        [TestMethod]
        public void ParseLimitOverrunResponseTilt()
        {
            var handler = new FeedbackHandler();
            var limitOverrunType = handler.GetLimitOverrunType(0x4C, 0x54, 0x54);
            Assert.AreEqual(LimitOverrunType.Tilt, limitOverrunType);
        }

        [TestMethod]
        public void ParseLimitOverrunResponseInvalid()
        {
            var handler = new FeedbackHandler();
            var limitOverrunType = handler.GetLimitOverrunType(0x4C, 0x00, 0x00);
            Assert.AreEqual(LimitOverrunType.Unknown, limitOverrunType);
        }
    }
}
