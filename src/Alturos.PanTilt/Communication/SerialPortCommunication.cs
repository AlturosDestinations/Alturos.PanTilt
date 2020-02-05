#if NET461

using log4net;
using System;
using System.IO.Ports;
using System.Linq;

namespace Alturos.PanTilt.Communication
{
    public class SerialPortCommunication : ICommunication
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SerialPortCommunication));
        private readonly SerialPort _serialPort;
        public event Action<byte[]> ReceiveData;
        public event Action<byte[], string> SendData;

        public SerialPortCommunication(string comPort)
        {
            Log.Debug($"{nameof(SerialPortCommunication)} - {comPort} 19200, Parity.None, Data bits:8, Stop bits:1");
            this._serialPort = new SerialPort(comPort, 19200, Parity.None, 8, StopBits.One);
            this._serialPort.DataReceived += DataReceived;
            if (this._serialPort.IsOpen)
            {
                Log.Error($"{nameof(SerialPortCommunication)} - Port is already open");
                return;
            }

            this._serialPort.Open();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._serialPort == null)
            {
                return;
            }

            this._serialPort.DataReceived -= DataReceived;
            if (this._serialPort.IsOpen)
            {
                this._serialPort.Close();
            }

            this._serialPort.Dispose();
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var buffer = new byte[128];

            var serialPort = (SerialPort)sender;
            var receiveCount = serialPort.Read(buffer, 0, buffer.Length);
            this.ReceiveData?.Invoke(buffer.Take(receiveCount).ToArray());

            //var hex = BitConverter.ToString(buffer.Take(receiveCount).ToArray());
            //Log.Debug($"{nameof(DataReceived)} - {hex}");
        }

        public bool Send(byte[] data, string description)
        {
            this._serialPort.Write(data, 0, data.Length);
            this.SendData?.Invoke(data, description);
            return true;
        }

        public string ReadExisting()
        {
            return this._serialPort.ReadExisting();
        }
    }
}

#else

using Alturos.PanTilt.Communication;
using System;

public class SerialPortCommunication : ICommunication
{
    public event Action<byte[]> ReceiveData;
    public event Action<byte[], string> SendData;

    public SerialPortCommunication()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public bool Send(byte[] data, string description)
    {
        throw new NotImplementedException();
    }
}

#endif
