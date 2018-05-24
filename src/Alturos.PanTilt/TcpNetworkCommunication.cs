using log4net;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Alturos.PanTilt
{
    public class TcpNetworkCommunication : ICommunication
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TcpNetworkCommunication));
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private IPEndPoint _ipEndPoint;
        public event Action<byte[]> ReceiveData;
        public event Action<byte[], string> SendData;

        public TcpNetworkCommunication(IPEndPoint ipEndPoint)
        {
            this._tcpClient = new TcpClient();
            this._tcpClient.Connect(ipEndPoint);
            this._ipEndPoint = ipEndPoint;
            this._networkStream = this._tcpClient.GetStream();

            Task.Run(() => ReceiveProcess());
        }

        public void Dispose()
        {
            this._networkStream?.Close();
            this._networkStream?.Dispose();
            this._tcpClient?.Close();
            this._tcpClient?.Dispose();
        }

        public bool Send(byte[] data, string description)
        {
            if (!this._networkStream.CanWrite)
            {
                return false;
            }

            this._networkStream.Write(data, 0, data.Length);
            this.SendData?.Invoke(data, description);
            return true;
        }

        private async void ReceiveProcess()
        {
            try
            {
                if (!this._tcpClient.Connected)
                {
                    return;
                }

                if (!this._networkStream.CanRead)
                {
                    this.ReceiveProcess();
                    return;
                }

                var buffer = new byte[128];
                var receiveCount = await this._networkStream.ReadAsync(buffer, 0, buffer.Length);
                this.ReceiveData?.Invoke(buffer.Take(receiveCount).ToArray());
                this.ReceiveProcess();
            }
            catch (ObjectDisposedException)
            {
                //Default for Dispose
            }
            catch (Exception exception)
            {
                Log.Error(nameof(ReceiveProcess), exception);
            }
        }
    }
}
