namespace Alturos.PanTilt.TestUI.Model
{
    public class DiscoveredDevice
    {
        public string Name { get { return $"{this.Manufacturer} {this.IpAddress}"; } }
        public string Manufacturer { get; set; }
        public string IpAddress { get; set; }
    }
}
