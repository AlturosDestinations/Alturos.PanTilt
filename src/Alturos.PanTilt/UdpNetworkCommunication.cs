using log4net;
using System;
using System.Net;
using System.Net.Sockets;

namespace Alturos.PanTilt
{
    public class UdpNetworkCommunication : ICommunication
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UdpNetworkCommunication));
        private UdpClient _receiver;
        private UdpClient _sender;
        private readonly IPEndPoint _ipEndPoint;
        private readonly bool _externalReceiver;

        public event Action<byte[]> ReceiveData;
        public event Action<byte[], string> SendData;

        public UdpNetworkCommunication(IPAddress ipAddress, UdpClient receiver, int sendPort = 4003)
        {
            this._externalReceiver = true;
            this._ipEndPoint = new IPEndPoint(ipAddress, sendPort);
            this.Initialize(ipAddress, sendPort, receiver);
        }

        public UdpNetworkCommunication(IPAddress ipAddress, int sendPort = 4003, int receivePort = 4003)
        {
            this._ipEndPoint = new IPEndPoint(ipAddress, receivePort);
            var receiver = new UdpClient(receivePort, AddressFamily.InterNetwork);

            this.Initialize(ipAddress, sendPort, receiver);
        }

        private void Initialize(IPAddress ipAddress, int sendPort = 4003, UdpClient receiver = null)
        {
            this._sender = new UdpClient();
            this._sender.Connect(ipAddress, sendPort);
            this._receiver = receiver;

            try
            {
                this._receiver.BeginReceive(new AsyncCallback(ReceiveProcess), null);
            }
            catch (Exception exception)
            {
                Log.Error(nameof(Initialize), exception);
            }
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
                this._receiver?.Close();
                this._receiver?.Dispose();
            }
        }

        public bool Send(byte[] data, string description)
        {
            this._sender.Send(data, data.Length);
            this.SendData?.Invoke(data, description);
            return true;
        }

        private void ReceiveProcess(IAsyncResult result)
        {
            IPEndPoint remoteIpEndPoint = null;

            try
            {
                var received = this._receiver.EndReceive(result, ref remoteIpEndPoint);

                if (this._ipEndPoint.Address.Equals(remoteIpEndPoint.Address))
                {
                    this.ReceiveData?.Invoke(received);
                }
                else
                {
                    Log.Debug($"{nameof(ReceiveProcess)} - Invalid endpoint {remoteIpEndPoint.Address}");
                }

                this._receiver.BeginReceive(new AsyncCallback(ReceiveProcess), null);
            }
            catch (ObjectDisposedException exception)
            {
                Log.Debug(nameof(ReceiveProcess), exception);
            }
            catch (Exception exception)
            {
                Log.Error(nameof(ReceiveProcess), exception);
            }
        }
    }
}
