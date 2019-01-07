using System;
using System.Text;
using System.Threading;

namespace Alturos.PanTilt.Eneo
{
    public class FirmwareReader : IDisposable
    {
        private readonly ICommunication _communication;
        public string Firmware { get; private set; }

        public FirmwareReader(ICommunication communication)
        {
            this._communication = communication;
            this._communication.ReceiveData += DataReceive;

            var getFirmwareCommand = new byte[] { 0x50, 0x80, 0x81, 0x69, 0x00 };
            this._communication.Send(getFirmwareCommand, "GetFirmware");

            Thread.Sleep(500);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this._communication.ReceiveData -= DataReceive;
        }

        private void DataReceive(byte[] data)
        {
            var firmare = Encoding.ASCII.GetString(data);
            this.Firmware += firmare.Trim();
        }
    }
}
