using Alturos.PanTilt.TestUI.Model;
using System;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace Alturos.PanTilt.TestUI.Dialog
{
    public partial class ConfigDialog : Form
    {
        public DeviceConfiguration DeviceConfiguration;

        public ConfigDialog(DeviceConfiguration deviceConfiguration)
        {
            this.InitializeComponent();

            this.comboBoxComType.DataSource = ((CommunicationType[])Enum.GetValues(typeof(CommunicationType))).OrderBy(x => x.ToString()).ToList();
            this.comboBoxPort.DataSource = SerialPort.GetPortNames();

            if (deviceConfiguration == null)
            {
                this.DeviceConfiguration = new DeviceConfiguration();
            }
            else
            {
                this.DeviceConfiguration = deviceConfiguration;
                this.comboBoxComType.SelectedItem = this.DeviceConfiguration.CommunicationType;
                this.textBoxPanTilt.Text = this.DeviceConfiguration.PanTiltIpAddress;
                this.comboBoxPort.Text = this.DeviceConfiguration.ComPort;
                this.checkBoxCameraActive.Checked = this.DeviceConfiguration.CameraActive;
                this.textBoxCameraIpAddress.Text = this.DeviceConfiguration.CameraIpAddress;
                this.textBoxCameraJpegUrl.Text = this.DeviceConfiguration.CameraJpegUrl;
                this.textBoxCameraIpAddress.Text = this.DeviceConfiguration.CameraIpAddress;
            }
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            this.DeviceConfiguration.CommunicationType = (CommunicationType)this.comboBoxComType.SelectedItem;
            this.DeviceConfiguration.PanTiltIpAddress = this.textBoxPanTilt.Text;
            this.DeviceConfiguration.ComPort = this.comboBoxPort.Text;
            this.DeviceConfiguration.CameraActive = this.checkBoxCameraActive.Checked;
            this.DeviceConfiguration.CameraIpAddress = this.textBoxCameraIpAddress.Text;
            this.DeviceConfiguration.CameraJpegUrl = this.textBoxCameraJpegUrl.Text;
            this.DeviceConfiguration.CameraIpAddress = this.textBoxCameraIpAddress.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBox_ComType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = (CommunicationType) this.comboBoxComType.SelectedItem;
            var serialPortSelected = selectedValue.Equals(CommunicationType.SerialPort);

            this.comboBoxPort.Visible = serialPortSelected;
            this.labelCOMPort.Visible = serialPortSelected;
            this.textBoxPanTilt.Visible = !serialPortSelected;
            this.labelPTIP.Visible = !serialPortSelected;
        }
    }
}
