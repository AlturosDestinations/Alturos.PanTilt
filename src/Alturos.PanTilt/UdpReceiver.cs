using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using log4net;

namespace Alturos.PanTilt
{
    public class UdpReceiver : IDisposable
    {
        private static ILog Log = LogManager.GetLogger(typeof(UdpReceiver));

        public UdpClient UdpClient { get; private set; }
        private readonly IPAddress[] _allowedIpAddress;

        public event Action<IPAddress, byte[]> OnPackageReceived;

        public UdpReceiver(int receivePort, IPAddress[] allowedIpAddresses)
        {
            this.UdpClient = new UdpClient(receivePort);
            this._allowedIpAddress = allowedIpAddresses;

            try
            {
                this.UdpClient.BeginReceive(new AsyncCallback(this.ReceiveProcess), null);
            }
            catch (Exception exception)
            {
                Log.Error(nameof(UdpReceiver), exception);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.UdpClient?.Dispose();
        }

        private void ReceiveProcess(IAsyncResult result)
        {
            IPEndPoint remoteIpEndPoint = null;

            try
            {
                var received = this.UdpClient.EndReceive(result, ref remoteIpEndPoint);

                if (this._allowedIpAddress.Contains(remoteIpEndPoint.Address))
                {
                    this.OnPackageReceived?.Invoke(remoteIpEndPoint.Address, received);
                }
                else
                {
                    Log.Debug($"{nameof(ReceiveProcess)} - Package received from unallowed ip {remoteIpEndPoint.Address}");
                }

                this.UdpClient.BeginReceive(new AsyncCallback(this.ReceiveProcess), null);
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
