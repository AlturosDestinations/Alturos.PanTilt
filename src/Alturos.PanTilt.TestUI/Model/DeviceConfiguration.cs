namespace Alturos.PanTilt.TestUI.Model
{
    public class DeviceConfiguration
    {
        //Camera
        public bool CameraActive;
        public string CameraIpAddress;
        public string CameraJpegUrl;
        
        //PanTilt
        public CommunicationType CommunicationType;
        public string PanTiltIpAddress;
        public string ComPort;
    }
}
