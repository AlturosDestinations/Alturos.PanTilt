using System.Runtime.InteropServices;

namespace Alturos.PanTilt.Manufacturer.Alturos.Eprom
{
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct IpAddressData
    {
        public byte Part1;
        public byte Part2;
        public byte Part3;
        public byte Part4;

        public override string ToString()
        {
            return $"{this.Part1}.{this.Part2}.{this.Part3}.{this.Part4}";
        }
    }
}
