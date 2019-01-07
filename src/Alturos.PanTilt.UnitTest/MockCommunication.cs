using System;

namespace Alturos.PanTilt.UnitTest
{
    public class MockCommunication : ICommunication
    {
#pragma warning disable 0067
        public event Action<byte[]> ReceiveData;
#pragma warning restore 0067
        public event Action<byte[], string> SendData;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }

        public bool Send(byte[] data, string description)
        {
            this.SendData?.Invoke(data, description);
            return true;
        }
    }
}
