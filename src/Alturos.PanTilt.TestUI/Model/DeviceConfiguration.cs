using System;

namespace Alturos.PanTilt.TestUI.Model
{
    public class DeviceConfiguration
    {
        //Camera
        public bool CameraActive { get; set; }
        public string CameraIpAddress { get; set; }
        public string CameraJpegUrl { get; set; }

        //PanTilt
        public PanTiltControlType PanTiltControlType { get; set; }
        public CommunicationType CommunicationType { get; set; }
        public string PanTiltIpAddress { get; set; }
        public string ComPort { get; set; }
    }
}
