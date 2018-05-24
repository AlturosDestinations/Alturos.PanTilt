using System;

namespace Alturos.PanTilt.UnitTest
{
    public class MockCommunication : ICommunication
    {
        public event Action<byte[]> ReceiveData;
        public event Action<byte[], string> SendData;

        public void Dispose()
        {

        }

        public bool Send(byte[] data, string description)
        {
            this.SendData?.Invoke(data, description);
            return true;
        }
    }
}
