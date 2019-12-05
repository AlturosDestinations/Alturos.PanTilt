using Alturos.DeviceDiscovery;
using Alturos.PanTilt.Communication;
using Alturos.PanTilt.TestUI.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alturos.PanTilt.TestUI.Extension;

namespace Alturos.PanTilt.TestUI.Dialog
{
    public partial class ConfigDialog : Form
    {
        public DeviceConfiguration DeviceConfiguration;

        public ConfigDialog(DeviceConfiguration deviceConfiguration)
        {
            this.InitializeComponent();

            this.comboBoxCameraImageUrl.DataSource = new string[]
            {
                "/jpg/image.jpg", //Axis
                "/snapshot/1/snapshot.jpg" //Eneo
            };

            this.comboBoxPanTiltControl.DataSource = ((PanTiltControlType[])Enum.GetValues(typeof(PanTiltControlType))).OrderBy(x => x.ToString()).ToList();
            this.comboBoxComType.DataSource = ((CommunicationType[])Enum.GetValues(typeof(CommunicationType))).OrderBy(x => x.ToString()).ToList();
            this.comboBoxPort.DataSource = SerialPort.GetPortNames();

            if (deviceConfiguration == null)
            {
                this.DeviceConfiguration = new DeviceConfiguration();
            }
            else
            {
                this.DeviceConfiguration = deviceConfiguration;
                this.comboBoxPanTiltControl.SelectedItem = this.DeviceConfiguration.PanTiltControlType;
                this.comboBoxComType.SelectedItem = this.DeviceConfiguration.CommunicationType;
                this.textBoxPanTilt.Text = this.DeviceConfiguration.PanTiltIpAddress;
                this.comboBoxPort.Text = this.DeviceConfiguration.ComPort;
                this.checkBoxCameraActive.Checked = this.DeviceConfiguration.CameraActive;
                this.textBoxCameraIpAddress.Text = this.DeviceConfiguration.CameraIpAddress;
                this.comboBoxCameraImageUrl.Text = this.DeviceConfiguration.CameraJpegUrl;
                this.textBoxCameraIpAddress.Text = this.DeviceConfiguration.CameraIpAddress;
            }

            this.comboBoxDiscoverd.DisplayMember = "Name";
            this.comboBoxDiscoverd.ValueMember = "IpAddress";
            _ = Task.Run(async () => await this.DiscoverDevicesAsync());
        }

        private async Task DiscoverDevicesAsync()
        {
            var detection = new UdpDeviceDetection();

            var packagesAlturos = await detection.GetDeviceInfoPackagesAsync(5555, new byte[] { 0x43, 0x30, 0x32 }, timeout: 1000);
            var packagesEneo = await detection.GetDeviceInfoPackagesAsync(4800, new byte[] { 0x01, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00 }, timeout: 1000);

            var discoveredDevices = new List<DiscoveredDevice>();
            discoveredDevices.Add(new DiscoveredDevice { Manufacturer = "Choose a device" });
            discoveredDevices.AddRange(packagesAlturos.Select(o => new DiscoveredDevice { Manufacturer = "Alturos", IpAddress = o.DeviceIpAddress }));
            discoveredDevices.AddRange(packagesEneo.GroupBy(o => o.DeviceIpAddress).Select(o => new DiscoveredDevice { Manufacturer = "Eneo", IpAddress = o.First().DeviceIpAddress }));

            this.comboBoxDiscoverd.Invoke(o => o.DataSource = discoveredDevices);
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            this.DeviceConfiguration.PanTiltControlType = (PanTiltControlType)this.comboBoxPanTiltControl.SelectedItem;
            this.DeviceConfiguration.CommunicationType = (CommunicationType)this.comboBoxComType.SelectedItem;
            this.DeviceConfiguration.PanTiltIpAddress = this.textBoxPanTilt.Text;
            this.DeviceConfiguration.ComPort = this.comboBoxPort.Text;
            this.DeviceConfiguration.CameraActive = this.checkBoxCameraActive.Checked;
            this.DeviceConfiguration.CameraIpAddress = this.textBoxCameraIpAddress.Text;
            this.DeviceConfiguration.CameraJpegUrl = this.comboBoxCameraImageUrl.Text;
            this.DeviceConfiguration.CameraIpAddress = this.textBoxCameraIpAddress.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBox_ComType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = (CommunicationType) this.comboBoxComType.SelectedItem;
            var serialPortSelected = selectedValue.Equals(CommunicationType.SerialPort);

            this.comboBoxPort.Visible = serialPortSelected;
            this.labelComPort.Visible = serialPortSelected;
            this.textBoxPanTilt.Visible = !serialPortSelected;
            this.labelPanTiltIpAddress.Visible = !serialPortSelected;
        }

        private void comboBoxDiscoverd_SelectedIndexChanged(object sender, EventArgs e)
        {
            var device = this.comboBoxDiscoverd.SelectedItem as DiscoveredDevice;
            if (string.IsNullOrEmpty(device.IpAddress))
            {
                return;
            }

            if (device.Manufacturer.Equals("Eneo", StringComparison.OrdinalIgnoreCase))
            {
                this.comboBoxPanTiltControl.SelectedItem = PanTiltControlType.Eneo;
            }
            else
            {
                this.comboBoxPanTiltControl.SelectedItem = PanTiltControlType.Alturos;
            }
            
            this.comboBoxComType.SelectedItem = CommunicationType.NetworkUdp;
            this.textBoxPanTilt.Text = device.IpAddress;
        }
    }
}
