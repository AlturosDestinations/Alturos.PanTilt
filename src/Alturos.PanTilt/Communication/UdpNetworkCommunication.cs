using System;
using System.Net;
using System.Net.Sockets;

namespace Alturos.PanTilt.Communication
{
    public class UdpNetworkCommunication : ICommunication
    {
        private UdpReceiver _receiver;
        private UdpClient _sender;
        private IPAddress _ipAddress;
        private readonly bool _externalReceiver;

        public event Action<byte[]> ReceiveData;
        public event Action<byte[], string> SendData;

        public UdpNetworkCommunication(IPAddress ipAddress, UdpReceiver receiver, int sendPort)
        {
            this._externalReceiver = true;
            this.Initialize(ipAddress, sendPort, receiver);
        }

        public UdpNetworkCommunication(IPAddress ipAddress, int sendPort, int receivePort)
        {
            var receiver = new UdpReceiver(receivePort, new IPAddress[] { ipAddress });
            this.Initialize(ipAddress, sendPort, receiver);
        }

        public UdpNetworkCommunication(IPAddress ipAddress, int sendPort)
        {
            this.Initialize(ipAddress, sendPort);
        }

        private void Initialize(IPAddress ipAddress, int sendPort, UdpReceiver receiver = null)
        {
            this._ipAddress = ipAddress;
            this._sender = new UdpClient();
            this._sender.Connect(ipAddress, sendPort);

            var localIpEndPoint = (IPEndPoint)this._sender.Client.LocalEndPoint;
            if (receiver == null)
            {
                this._receiver = new UdpReceiver(localIpEndPoint, new IPAddress[] { ipAddress });
            }
            else
            {
                this._receiver = receiver;
            }
            
            this._receiver.OnPackageReceived += OnPackageReceived;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this._sender?.Close();
            this._sender?.Dispose();

            if (!this._externalReceiver)
            {
                this._receiver?.UdpClient?.Close();
                this._receiver?.UdpClient?.Dispose();
            }
        }

        public bool Send(byte[] data, string description)
        {
            this._sender.Send(data, data.Length);
            this.SendData?.Invoke(data, description);
            return true;
        }

        private void OnPackageReceived(IPAddress ipAddress, byte[] bytes)
        {
            if (this._ipAddress.Equals(ipAddress))
            {
                this.ReceiveData?.Invoke(bytes);
            }
        }
    }
}
