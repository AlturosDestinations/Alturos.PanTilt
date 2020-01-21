using Alturos.PanTilt.Tools;
using System.Runtime.InteropServices;

namespace Alturos.PanTilt.Manufacturer.Alturos.Eprom
{
    [StructLayout(LayoutKind.Sequential, Size = 6)]
    public struct MacData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Data;

        public override string ToString()
        {
            return $"{ByteConverter.ByteArrayToHex(this.Data)}";
        }
    }
}
