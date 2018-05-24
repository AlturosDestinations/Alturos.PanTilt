using System;

namespace Alturos.PanTilt
{
    public interface ICommunication : IDisposable
    {
        event Action<byte[]> ReceiveData;
        event Action<byte[], string> SendData;

        bool Send(byte[] data, string description);
    }
}
