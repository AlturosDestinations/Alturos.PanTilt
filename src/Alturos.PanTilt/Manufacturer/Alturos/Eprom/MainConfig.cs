using System.Runtime.InteropServices;

namespace Alturos.PanTilt.Manufacturer.Alturos.Eprom
{
    [StructLayout(LayoutKind.Sequential, Size = 128, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MainConfig
    {
        public byte Version1;
        public byte Version2;
        public byte ImgStat;
        public byte Sig1;
        public byte Sig2;
        public IpAddressData Gateway { get; set; }
        public uint Sn { get; set; }
        public MacData Mac { get; set; }
        public IpAddressData Ip { get; set; }

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 42)]
        public byte[] UnimportantData1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public byte[] UnimportantData2;

        public ushort PanSensor0Min { get; set; }
        public ushort PanSensor1Min { get; set; }
        public ushort PanSensor0Max { get; set; }
        public ushort PanSensor1Max { get; set; }
        public float PanAngleOffset { get; set; }
        public ushort TiltSensor0Min { get; set; }
        public ushort TiltSensor1Min { get; set; }
        public ushort TiltSensor0Max { get; set; }
        public ushort TiltSensor1Max { get; set; }
        public float TiltAngleOffset { get; set; }
        public byte TemperatureSensorEnabled { get; set; }
        //Not change this value
        public byte PanTiltConfigBits;
        public ushort PanRampStartDely { get; set; }
        public ushort TiltRampStartDely { get; set; }
        public float PanRampAccel { get; set; }
        public float TiltRampAccel { get; set; }
        public float PanAxisOffset { get; set; }
        public float TiltAxisOffset { get; set; }
        public byte PanInititalSpeed { get; set; }
        public byte TiltInitialSpeed { get; set; }
    }
}
