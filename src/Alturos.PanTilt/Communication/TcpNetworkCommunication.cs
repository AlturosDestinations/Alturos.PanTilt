using log4net;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Alturos.PanTilt.Communication
{
    public class TcpNetworkCommunication : ICommunication
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TcpNetworkCommunication));
        private readonly IPEndPoint _ipEndPoint;
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        public event Action<byte[]> ReceiveData;
        public event Action<byte[], string> SendData;

        public TcpNetworkCommunication(IPEndPoint ipEndPoint)
        {
            this._ipEndPoint = ipEndPoint;
            this.Connect();

            Task.Run(async () => await this.ReceiveProcessAsync());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this._networkStream?.Close();
            this._networkStream?.Dispose();
            this._tcpClient?.Close();
            this._tcpClient?.Dispose();
        }

        private void Connect()
        {
            if (this._networkStream != null)
            {
                try
                {
                    this._networkStream.Close();
                    this._networkStream.Dispose();
                }
                catch (Exception exception)
                {
                    Log.Error($"{nameof(Connect)} - NetworkStream cleanup", exception);
                }
            }

            if (this._tcpClient != null)
            {
                try
                {
                    this._tcpClient.Close();
                    this._tcpClient.Dispose();
                }
                catch (Exception exception)
                {
                    Log.Error($"{nameof(Connect)} - TcpClient cleanup", exception);
                }
            }

            this._tcpClient = new TcpClient();
            this._tcpClient.Connect(this._ipEndPoint);
            this._networkStream = this._tcpClient.GetStream();

            Log.Debug($"{nameof(Connect)} - {this._ipEndPoint}");
        }

        public bool Send(byte[] data, string description)
        {
            if (!this._tcpClient.Connected)
            {
                Task.Run(() => this.Connect());
                return false;
            }

            if (!this._networkStream.CanWrite)
            {
                return false;
            }

            this._networkStream.Write(data, 0, data.Length);
            this.SendData?.Invoke(data, description);
            return true;
        }

        private async Task ReceiveProcessAsync()
        {
            try
            {
                if (!this._tcpClient.Connected)
                {
                    return;
                }

                if (!this._networkStream.CanRead)
                {
                    await Task.Run(async () => await this.ReceiveProcessAsync());
                    return;
                }

                var buffer = new byte[128];
                var receiveCount = await this._networkStream.ReadAsync(buffer, 0, buffer.Length);
                this.ReceiveData?.Invoke(buffer.Take(receiveCount).ToArray());
                await Task.Run(async () => await this.ReceiveProcessAsync());
            }
            catch (ObjectDisposedException)
            {
                //Default for Dispose
            }
            catch (Exception exception)
            {
                Log.Error(nameof(ReceiveProcessAsync), exception);
            }
        }
    }
}
