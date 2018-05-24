using System;

namespace Alturos.PanTilt.TestUI.Model
{
    public class DataPackage
    {
        public DateTime Timestamp { get; }
        public byte[] Data { get; }
        public string DataReadable { get; }
        public string Type { get; set; }

        public DataPackage(byte[] data)
        {
            var hex = BitConverter.ToString(data);

            this.Timestamp = DateTime.Now;
            this.Data = data;
            this.DataReadable = hex;
        }

        public DataPackage(byte[] data, string description) : this(data)
        {
            this.Type = description;
        }
    }
}
