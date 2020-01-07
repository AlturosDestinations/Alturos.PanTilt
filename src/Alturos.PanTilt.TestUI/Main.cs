using Accord.Video;
using Alturos.PanTilt.Communication;
using Alturos.PanTilt.Diagnostic;
using Alturos.PanTilt.TestUI.Dialog;
using Alturos.PanTilt.TestUI.Extension;
using Alturos.PanTilt.TestUI.Model;
using Alturos.PanTilt.Tools;
using Alturos.PanTilt.Translator;
using System;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alturos.PanTilt.TestUI
{
    public partial class Main : Form
    {
        private ICommunication _communication;
        private IPanTiltControl _panTiltControl;
        private bool _mouseControlActive;
        private Accord.Controls.VideoSourcePlayer _videoSourcePlayer;
        private DateTime _lastRefresh;
        private DeviceConfiguration _deviceConfiguration;
        private DeviceConfigurationHelper _deviceConfigurationHelper;
        private DrawEngine _cameraDrawEngine;
        private IPositionChecker _positionChecker;

        public Main()
        {
            this.InitializeComponent();

            this._deviceConfigurationHelper = new DeviceConfigurationHelper();
            this._deviceConfiguration = this._deviceConfigurationHelper.LoadConfig("default");

            using (var dialog = new ConfigDialog(_deviceConfiguration))
            {
                dialog.StartPosition = FormStartPosition.CenterParent;
                var dialogResult = dialog.ShowDialog(this);
                switch (dialogResult)
                {
                    case DialogResult.OK:
                        this._deviceConfiguration = dialog.DeviceConfiguration;
                        this._deviceConfigurationHelper.SaveConfig("default", this._deviceConfiguration);
                        break;
                    case DialogResult.Cancel:
                    case DialogResult.None:
                        Task.Run(() => this.Invoke(o => o.Close()));
                        return;
                }
            }

            this.Text = $"Alturos PanTilt TestUI - v{Application.ProductVersion}";
            this.labelPositionPan.Text = "Pan: ?,??";
            this.labelPositionTilt.Text = "Tilt: ?,??";

            this.SetConfigurationInfo();

            var startPtHeadCommunication = Task.Run(() => this.StartPanTiltCommunication());

            this.UpdateMousePanel();

            if (this._deviceConfiguration.CameraActive)
            {
                //Live Camera Image
                var url = $"http://{this._deviceConfiguration.CameraIpAddress}{this._deviceConfiguration.CameraJpegUrl}";

                IVideoSource source = new JPEGStream(url);
                ((JPEGStream)source).FrameInterval = 200;

                this._videoSourcePlayer = new Accord.Controls.VideoSourcePlayer
                {
                    VideoSource = source,
                    Dock = DockStyle.Fill
                };
                this._videoSourcePlayer.Start();
                this.tabPageLiveView.Controls.Add(this._videoSourcePlayer);
            }
            else
            {
                //Visual Map
                this._cameraDrawEngine = new DrawEngine(4);
                this.UpdateCurrentImage();
                this.pictureBox_CameraPos.Visible = true;
            }

            startPtHeadCommunication.Wait();

            if (this._panTiltControl != null)
            {
                this.continiousMovementControl1.SetPanTiltControl(this._panTiltControl);
                this.fastMovementControl1.SetPanTiltControl(this._panTiltControl);
                this.absolutePositionControl1.SetPanTiltControl(this._panTiltControl);
                this.eneoUserControl1.SetPanTiltControl(this._panTiltControl);
                this.alturosUserControl1.SetPanTiltControl(this._panTiltControl);
                this.movementFloodControl1.SetPanTiltControl(this._panTiltControl);
                this.commandSequenceControl1.SetPanTiltControl(this._panTiltControl);
                this.alturosUserControl1.SetDeviceConfiguration(this._deviceConfiguration);
                this._panTiltControl.PanTiltAbsolute(0,0);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._videoSourcePlayer != null)
            {
                this._videoSourcePlayer.Stop();
                this._videoSourcePlayer.Dispose();
            }

            if (this._panTiltControl != null)
            {
                this._panTiltControl.Stop();
                this._panTiltControl.PositionChanged -= OnPositionChanged;
                this._panTiltControl.LimitChanged -= OnLimitChanged;
                this._panTiltControl.Dispose();
            }

            this._communication?.Dispose();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            this.groupBoxControls.Location = new Point(this.mainTabControl.Width - this.groupBoxControls.Width - 10, this.mainTabControl.Height - this.groupBoxControls.Height - 20);
        }

        private void SetConfigurationInfo()
        {
            this.labelCameraIpAddress.Text = $"No Camera available";
            if (this._deviceConfiguration.CameraActive)
            {
                this.labelCameraIpAddress.Text = $"Camera: {this._deviceConfiguration.CameraIpAddress}";
            }

            if (this._deviceConfiguration.CommunicationType == CommunicationType.SerialPort)
            {
                this.labelPanTiltIpAddress.Text = $"PanTilt: {this._deviceConfiguration.ComPort}";
            }
            else
            {
                this.labelPanTiltIpAddress.Text = $"PanTilt: {this._deviceConfiguration.PanTiltIpAddress}";
            }
        }

        #region Communication

        private async Task StartPanTiltCommunication()
        {
            if (string.IsNullOrEmpty(this._deviceConfiguration.PanTiltIpAddress))
            {
                return;
            }

            try
            {
                switch (this._deviceConfiguration.CommunicationType)
                {
                    case CommunicationType.NetworkTcp:
                        this._communication = new TcpNetworkCommunication(new IPEndPoint(IPAddress.Parse(this._deviceConfiguration.PanTiltIpAddress), 4003));
                        break;
                    case CommunicationType.NetworkUdp:
                        var port = 4003;
                        if (this._deviceConfiguration.PanTiltControlType == PanTiltControlType.Alturos)
                        {
                            port = 5555;
                        }

                        this._communication = new UdpNetworkCommunication(IPAddress.Parse(this._deviceConfiguration.PanTiltIpAddress), port, port);
                        break;
                    case CommunicationType.SerialPort:
                        this._communication = new SerialPortCommunication(this._deviceConfiguration.ComPort);
                        break;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Cannot initialize communication, error:{exception.ToString()}", "Communication error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._communication.SendData += this.CommunicationSendData;
            this._communication.ReceiveData += this.CommunicationReceiveData;

            switch (this._deviceConfiguration.PanTiltControlType)
            {
                case PanTiltControlType.Alturos:
                    this.mainTabControl.TabPages.Remove(this.tabPageEneo);
                    this._panTiltControl = new AlturosPanTiltControl(this._communication);
                    this.communicationHistoryControl1.SetTranslator(new AlturosFeedbackTranslator());
                    break;
                case PanTiltControlType.Eneo:
                    this.mainTabControl.TabPages.Remove(this.tabPageAlturos);
                    this._panTiltControl = new EneoPanTiltControl(this._communication);
                    this.communicationHistoryControl1.SetTranslator(new EneoFeedbackTranslator());
                    break;
            }

            if (this._panTiltControl is IFirmwareReader firmwareReader)
            {
                var firmware = await firmwareReader.GetFirmwareAsync();
                this.labelFirmware.Invoke(o => o.Text = $"Firmware: {firmware}");
            }
            else
            {
                this.labelFirmware.Invoke(o => o.Text = $"Cannot get firmware info");
            }

            this._panTiltControl.PositionChanged += OnPositionChanged;
            this._panTiltControl.LimitChanged += OnLimitChanged;

            this._positionChecker = new PositionChecker(this._panTiltControl);

            this._panTiltControl.Start();

            Thread.Sleep(500);
            this.GetLimitInfos();
        }

        private void CommunicationReceiveData(byte[] data)
        {
            this.communicationHistoryControl1.AddReceivePackage(new DataPackage(data));
        }

        private void CommunicationSendData(byte[] data, string description)
        {
            this.communicationHistoryControl1.AddSendPackage(new DataPackage(data, description));
        }

        #endregion

        #region Manual Control

        private void buttonPanRelative_Click(object sender, EventArgs e)
        {
            double.TryParse(this.textBoxPan.Text, out var pan);
            this._panTiltControl.PanRelative(pan);
        }

        private void buttonPanAbsolute_Click(object sender, EventArgs e)
        {
            double.TryParse(this.textBoxPan.Text, out var pan);
            this._panTiltControl.PanAbsolute(pan);
        }

        private void buttonTiltAbsolute_Click(object sender, EventArgs e)
        {
            double.TryParse(this.textBoxTilt.Text, out var tilt);
            this._panTiltControl.TiltAbsolute(tilt);
        }

        private void buttonTiltRelative_Click(object sender, EventArgs e)
        {
            double.TryParse(this.textBoxTilt.Text, out var tilt);
            this._panTiltControl.TiltRelative(tilt);
        }

        private void buttonStopMoving_Click(object sender, EventArgs e)
        {
            this._panTiltControl.StopMoving();
        }

        private void buttonMoveToStartPosition_Click(object sender, EventArgs e)
        {
            this._panTiltControl.PanTiltAbsolute(0, 0);
        }

        #endregion

        #region Mouse Control

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            var sesnitive = 100 - this.trackBarSesnitive.Value;
            var factor = sesnitive / 10.0;

            var width = this.panelMouseControl.Width;
            var height = this.panelMouseControl.Height;

            var pan = e.X - width / (double)2;
            var tilt = -(e.Y - height / (double)2);

            pan = Math.Round(pan / factor, 2);
            tilt = Math.Round(tilt / factor, 2);

            if (pan >= 100)
            {
                pan = 100;
            }

            if (pan <= -100)
            {
                pan = -100;
            }

            if (tilt >= 100)
            {
                tilt = 100;
            }

            if (tilt <= -100)
            {
                tilt = -100;
            }

            this.labelSpeed.Text = $"Pan: {pan} °/s Tilt: {tilt} °/s";

            if (!this._mouseControlActive)
            {
                return;
            }

            this._panTiltControl?.PanTiltRelative(pan, tilt);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            this._mouseControlActive = !this._mouseControlActive;
            this.UpdateMousePanel();
        }

        private void UpdateMousePanel()
        {
            if (this._mouseControlActive)
            {
                this.panelMouseControl.BackColor = Color.DarkGreen;
            }
            else
            {
                this._panTiltControl?.StopMoving();
                this.panelMouseControl.BackColor = Color.DarkRed;
            }
        }

        #endregion

        #region Pan Tilt Head Limits

        private async void buttonRefreshLimitInfos_Click(object sender, EventArgs e)
        {
            await this.RefreshLimits();
        }

        private async Task RefreshLimits()
        {
            if (this._panTiltControl is EneoPanTiltControl eneoPanTiltControl)
            {
                eneoPanTiltControl.QueryLimits();
                await Task.Delay(1000);
            }
            
            this.GetLimitInfos();
        }

        private void buttonReinitialize_Click(object sender, EventArgs e)
        {
            this._panTiltControl.ReinitializePtHead();
        }

        private void GetLimitInfos()
        {
            var limits = this._panTiltControl.GetLimits();
            this.labelLimitLeft.Invoke(o => o.Text = $"Min:{limits.PanMin}");
            this.labelLimitRight.Invoke(o => o.Text = $"Max:{limits.PanMax}");
            this.labelLimitUp.Invoke(o => o.Text = $"Max:{limits.TiltMax}");
            this.labelLimitDown.Invoke(o => o.Text = $"Min:{limits.TiltMin}");
        }

        private void SetLimits(PanTiltLimit limits)
        {
            this._panTiltControl.SetLimits(limits);
        }

        private async void buttonSetLimits_Click(object sender, EventArgs e)
        {
            double.TryParse(this.textBoxLimitLeft.Text, out var panMin);
            double.TryParse(this.textBoxLimitRight.Text, out var panMax);
            double.TryParse(this.textBoxLimitUp.Text, out var tiltMax);
            double.TryParse(this.textBoxLimitDown.Text, out var tiltMin);

            #region Validation

            if (panMin >= 0)
            {
                MessageBox.Show("Invalid pan limit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (panMax <= 0)
            {
                MessageBox.Show("Invalid pan limit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tiltMin >= 0)
            {
                MessageBox.Show("Invalid tilt limit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tiltMax <= 0)
            {
                MessageBox.Show("Invalid tilt limit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            #endregion

            this.buttonSetLimits.Enabled = false;
            this.SetLimits(new PanTiltLimit { PanMin = panMin, PanMax = panMax, TiltMin = tiltMin, TiltMax = tiltMax });
            this.buttonSetLimits.Enabled = true;

            await this.RefreshLimits();
        }

        private void textBoxLimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (!double.TryParse(textBox.Text, out var temp))
                {
                    return;
                }

                if (e.KeyCode == Keys.Down)
                {
                    var newValue = temp -= 5;
                    textBox.Text = newValue.ToString();
                }

                if (e.KeyCode == Keys.Up)
                {
                    var newValue = temp += 5;
                    textBox.Text = newValue.ToString();
                }

                if (e.KeyCode == Keys.F1)
                {
                    this.textBoxLimitLeft.Text = "-40";
                    this.textBoxLimitRight.Text = "40";
                }

                if (e.KeyCode == Keys.F2)
                {
                    this.textBoxLimitLeft.Text = "-50";
                    this.textBoxLimitRight.Text = "50";
                }

                if (e.KeyCode == Keys.F3)
                {
                    this.textBoxLimitLeft.Text = "-60";
                    this.textBoxLimitRight.Text = "60";
                }

                if (e.KeyCode == Keys.F4)
                {
                    this.textBoxLimitLeft.Text = "-70";
                    this.textBoxLimitRight.Text = "70";
                }

                if (e.KeyCode == Keys.F5)
                {
                    this.textBoxLimitLeft.Text = "-80";
                    this.textBoxLimitRight.Text = "80";
                }

                if (e.KeyCode == Keys.F6)
                {
                    this.textBoxLimitLeft.Text = "-90";
                    this.textBoxLimitRight.Text = "90";
                }

                if (e.KeyCode == Keys.F7)
                {
                    this.textBoxLimitLeft.Text = "-100";
                    this.textBoxLimitRight.Text = "100";
                }

                if (e.KeyCode == Keys.F8)
                {
                    this.textBoxLimitLeft.Text = "-110";
                    this.textBoxLimitRight.Text = "110";
                }

                if (e.KeyCode == Keys.F9)
                {
                    this.textBoxLimitLeft.Text = "-120";
                    this.textBoxLimitRight.Text = "120";
                }

                if (e.KeyCode == Keys.F10)
                {
                    this.textBoxLimitLeft.Text = "-130";
                    this.textBoxLimitRight.Text = "130";
                }

                if (e.KeyCode == Keys.F11)
                {
                    this.textBoxLimitLeft.Text = "-140";
                    this.textBoxLimitRight.Text = "140";
                }

                if (e.KeyCode == Keys.F12)
                {
                    this.textBoxLimitLeft.Text = "-150";
                    this.textBoxLimitRight.Text = "150";
                }
            }
        }

        #endregion

        #region Other

        private void OnLimitChanged()
        {
            this.GetLimitInfos();
            var position = this._panTiltControl.GetPosition();
            this.Redraw(position);
        }

        private void OnPositionChanged(PanTiltPosition position)
        {
            if (this._lastRefresh.AddMilliseconds(200) >= DateTime.Now)
            {
                return;
            }

            this._lastRefresh = DateTime.Now;

            this.labelPositionPan.Invoke(o => o.Text = $"Pan: {position.Pan}");
            this.labelPositionTilt.Invoke(o => o.Text = $"Tilt: {position.Tilt}");

            if (this._deviceConfiguration.CameraActive)
            {
                return;
            }

            if (this._panTiltControl != null)
            {
                //TODO:Optimize render image does not block position changed event
                this.Redraw(position);
            }
        }

        private void Redraw(PanTiltPosition position)
        {
            if (this._cameraDrawEngine == null)
            {
                return;
            }

            this._cameraDrawEngine.Clear();
            this._cameraDrawEngine.DrawPtHeadLimits(this._panTiltControl.GetLimits());
            this._cameraDrawEngine.DrawCrossHair(position, Brushes.Black);
            this.UpdateCurrentImage();
        }

        private void UpdateCurrentImage()
        {
            var oldImage = this.pictureBox_CameraPos.Image;
            this.pictureBox_CameraPos.Image = this._cameraDrawEngine.GetImage();
            oldImage?.Dispose();
        }

        private void panelMouseControl_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.LightGray, 1))
            {
                using (var canvas = e.Graphics)
                {
                    canvas.DrawLine(pen, 0, this.panelMouseControl.Height / 2, this.panelMouseControl.Width, this.panelMouseControl.Height / 2);
                    canvas.DrawLine(pen, this.panelMouseControl.Width / 2, 0, this.panelMouseControl.Width / 2, this.panelMouseControl.Height);
                }
            }
        }

        #endregion

        #region Increase / Decrease buttons

        private void buttonDecreaseP_Click(object sender, EventArgs e)
        {
            double.TryParse(this.textBoxPan.Text, out var pan);
            pan--;
            this.textBoxPan.Text = pan.ToString();
        }

        private void buttonIncreaseP_Click(object sender, EventArgs e)
        {
            double.TryParse(this.textBoxPan.Text, out var pan);
            pan++;
            this.textBoxPan.Text = pan.ToString();
        }

        private void buttonDecreaseT_Click(object sender, EventArgs e)
        {
            double.TryParse(this.textBoxTilt.Text, out var tilt);
            tilt--;
            this.textBoxTilt.Text = tilt.ToString();
        }

        private void buttonIncreaseT_Click(object sender, EventArgs e)
        {
            double.TryParse(this.textBoxTilt.Text, out var tilt);
            tilt++;
            this.textBoxTilt.Text = tilt.ToString();
        }

        #endregion

        private async void button1_Click(object sender, EventArgs e)
        {
            var panTiltPosition = new PanTiltPosition(0, 0);

            this._panTiltControl.PanTiltAbsolute(panTiltPosition);
            await this._positionChecker.ComparePositionAsync(panTiltPosition);

            this._panTiltControl.PanTiltAbsolute(-14.25, 10.95);
            await Task.Delay(150);
            this._panTiltControl.PanTiltRelative(10, 5);
            await Task.Delay(40);
            this._panTiltControl.PanTiltRelative(10.2, 5.5);
            await Task.Delay(40);
            this._panTiltControl.PanTiltRelative(10.5, 6);
            await Task.Delay(40);
            this._panTiltControl.PanTiltAbsolute(-14.25, 8.95);
        }
    }
}
