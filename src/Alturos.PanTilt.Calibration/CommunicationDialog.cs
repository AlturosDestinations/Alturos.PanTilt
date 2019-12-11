using Alturos.PanTilt.Communication;
using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Alturos.PanTilt.Calibration
{
    public partial class CommunicationDialog : Form
    {
        public ICommunication Communication { get; private set; }
        public PanTiltControlType PanTiltControlType { get; private set; }

        public CommunicationDialog()
        {
            this.InitializeComponent();
            this.comboBoxCommunicationType.DataSource = Enum.GetValues(typeof(CommunicationType));
            this.comboBoxPanTiltControl.DataSource = ((PanTiltControlType[])Enum.GetValues(typeof(PanTiltControlType))).OrderBy(x => x.ToString()).ToList();
        }

        private void comboBoxConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connectionType = (CommunicationType)this.comboBoxCommunicationType.SelectedItem;

            switch (connectionType)
            {
                case CommunicationType.SerialPort:
                    this.label2.Text = "Serial Port:";
                    this.textBoxValue.Text = "COM1";
                    break;
                case CommunicationType.NetworkTcp:
                case CommunicationType.NetworkUdp:
                    this.label2.Text = "IpAddress:";
                    this.textBoxValue.Text = "192.168.184.35";
                    break;
                default:
                    break;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            var connectionType = (CommunicationType)this.comboBoxCommunicationType.SelectedItem;
            var panTiltControlType = (PanTiltControlType)this.comboBoxPanTiltControl.SelectedItem;

            this.PanTiltControlType = panTiltControlType;

            try
            {
                switch (connectionType)
                {
                    case CommunicationType.SerialPort:
                        this.Communication = new SerialPortCommunication(this.textBoxValue.Text);
                        break;
                    case CommunicationType.NetworkTcp:
                        this.Communication = new TcpNetworkCommunication(new IPEndPoint(IPAddress.Parse(this.textBoxValue.Text), 4003));
                        break;
                    case CommunicationType.NetworkUdp:
                        var port = 4003;
                        if (panTiltControlType == PanTiltControlType.Alturos)
                        {
                            port = 5555;
                        }

                        this.Communication = new UdpNetworkCommunication(IPAddress.Parse(this.textBoxValue.Text), port, port);
                        break;
                    default:
                        break;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Cannot connect {exception}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
