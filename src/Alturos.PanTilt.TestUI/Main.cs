using Accord.Video;
using Alturos.PanTilt.Contract;
using Alturos.PanTilt.Contract.Eneo;
using Alturos.PanTilt.Diagnostic;
using Alturos.PanTilt.TestUI.Contract;
using Alturos.PanTilt.TestUI.Dialog;
using Alturos.PanTilt.TestUI.Extension;
using Alturos.PanTilt.TestUI.Model;
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
        private IZoomProvider _zoomProvider;
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

            this.Text = $"Alturos PanTilt TestUI ({Application.ProductVersion})";
            this.SetConfigurationInfo();

            var startPtHeadCommunication = Task.Run(() => this.StartPanTiltCommunication());

            this.UpdateMousePanel();
            this.panelMouseControl.MouseWheel += MouseWheelZoom;

            //Disable TabPage Zoom - No ZoomProvider available
            //this.tabControl1.TabPages.Remove(this.tabPageCameraZoom);
            this._zoomProvider = new MockZoomProvider();
            this._zoomProvider.SetZoomAsync(0);
            this._zoomProvider.ZoomChanged += CameraControlZoomChanged;

            if (this._deviceConfiguration.CameraActive)
            {
                //Live Camera Image
                var url = $"http://{this._deviceConfiguration.CameraIpAddress}{this._deviceConfiguration.CameraJpegUrl}";

                IVideoSource source = new JPEGStream(url);
                ((JPEGStream)source).FrameInterval = 200;

                this._videoSourcePlayer = new Accord.Controls.VideoSourcePlayer();
                this._videoSourcePlayer.VideoSource = source;
                this._videoSourcePlayer.Start();
                this._videoSourcePlayer.Dock = DockStyle.Fill;
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

            this.panelMouseControl.MouseWheel -= MouseWheelZoom;

            if (this._panTiltControl != null)
            {
                this._panTiltControl.Stop();
                this._panTiltControl.PositionChanged -= OnPositionChanged;
                this._panTiltControl.Dispose();
            }

            this._communication?.Dispose();

            if (this._zoomProvider != null)
            {
                this._zoomProvider.ZoomChanged -= CameraControlZoomChanged;
                this._zoomProvider?.Dispose();
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            this.groupBoxControls.Location = new Point(this.mainTabControl.Width - this.groupBoxControls.Width - 10, this.mainTabControl.Height - this.groupBoxControls.Height - 20);
        }

        private void SetConfigurationInfo()
        {
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
                        this._communication = new UdpNetworkCommunication(IPAddress.Parse(this._deviceConfiguration.PanTiltIpAddress), 4003, 4003);
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

            using (var firmwareReader = new FirmwareReader(this._communication))
            {
                this.labelFirmware.Invoke(o => o.Text = $"Firmware: {firmwareReader.Firmware}");
                await Task.Delay(100).ConfigureAwait(false);
            }

            this._panTiltControl = new AlturosPanTiltControl(this._communication);
            //this._panTiltControl = new EneoPanTiltControl(this._communication);
            this._panTiltControl.PositionChanged += OnPositionChanged;
            this._positionChecker = new PositionChecker(this._panTiltControl);

            this._panTiltControl.Start();
            this.SetSmoothing(100, 50);

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

        #endregion

        #region Mouse Control

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            var zoom = this._zoomProvider.GetZoom();
            var factor = zoom / 10;

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

        #region Zoom

        private async void MouseWheelZoom(object sender, MouseEventArgs e)
        {
            if (this._zoomProvider == null)
            {
                return;
            }

            var currentZoom = this._zoomProvider.GetZoom();
            var zoomJump = 5;

            if (e.Delta == 120)
            {
                await this._zoomProvider.SetZoomAsync(currentZoom + zoomJump);
                return;
            }

            await this._zoomProvider.SetZoomAsync(currentZoom - zoomJump);
        }

        private void buttonZoom0_Click(object sender, EventArgs e)
        {
            this.trackBarZoom.Value = 0;
        }

        private void buttonZoom100_Click(object sender, EventArgs e)
        {
            this.trackBarZoom.Value = 100;
        }

        private void trackBarZoom_ValueChanged(object sender, EventArgs e)
        {
            this._zoomProvider?.SetZoomAsync(this.trackBarZoom.Value);
        }

        private void CameraControlZoomChanged(double zoom)
        {
            this.trackBarCurrentZoom.Invoke(o => o.Value = (int)zoom);
        }

        #endregion

        #region Pan Tilt Head Limits

        private void buttonEnableLimits_Click(object sender, EventArgs e)
        {
            var eneoPanTiltControl = this._panTiltControl as EneoPanTiltControl;
            if (eneoPanTiltControl == null)
            {
                return;
            }
            eneoPanTiltControl.EnableLimit();
        }

        private void buttonDisableLimits_Click(object sender, EventArgs e)
        {
            var eneoPanTiltControl = this._panTiltControl as EneoPanTiltControl;
            if (eneoPanTiltControl == null)
            {
                return;
            }
            eneoPanTiltControl.DisableLimit();
        }

        private async void buttonRefreshLimitInfos_Click(object sender, EventArgs e)
        {
            await this.RefreshLimits();
        }

        private async Task RefreshLimits()
        {
            var eneoPanTiltControl = this._panTiltControl as EneoPanTiltControl;
            if (eneoPanTiltControl == null)
            {
                return;
            }
            eneoPanTiltControl.QueryLimits();
            await Task.Delay(1000);
            this.GetLimitInfos();
        }

        private void buttonLimitUp_Click(object sender, EventArgs e)
        {
            var eneoPanTiltControl = this._panTiltControl as EneoPanTiltControl;
            if (eneoPanTiltControl == null)
            {
                return;
            }
            eneoPanTiltControl.SetLimitUp();
        }

        private void buttonSetLimitDown_Click(object sender, EventArgs e)
        {
            var eneoPanTiltControl = this._panTiltControl as EneoPanTiltControl;
            if (eneoPanTiltControl == null)
            {
                return;
            }
            eneoPanTiltControl.SetLimitDown();
        }

        private void buttonLimitLeft_Click(object sender, EventArgs e)
        {
            var eneoPanTiltControl = this._panTiltControl as EneoPanTiltControl;
            if (eneoPanTiltControl == null)
            {
                return;
            }
            eneoPanTiltControl.SetLimitLeft();
        }

        private void buttonSetLimitRight_Click(object sender, EventArgs e)
        {
            var eneoPanTiltControl = this._panTiltControl as EneoPanTiltControl;
            if (eneoPanTiltControl == null)
            {
                return;
            }
            eneoPanTiltControl.SetLimitRigth();
        }

        private void buttonReinitialize_Click(object sender, EventArgs e)
        {
            this._panTiltControl.ReinitializePosition();
        }

        private void GetLimitInfos()
        {
            var limits = this._panTiltControl.GetLimits();
            this.labelLimitLeft.Text = $"Min:{limits.PanMin}";
            this.labelLimitRight.Text = $"Max:{limits.PanMax}";
            this.labelLimitUp.Text = $"Max:{limits.TiltMax}";
            this.labelLimitDown.Text = $"Min:{limits.TiltMin}";
        }

        private async Task SetLimits(PanTiltLimit limits)
        {
            //Notice:
            //Pt head cannot move with absolute commands to a position outside the limits we must change the limits before with a relative command
            var eneoPanTiltControl = this._panTiltControl as EneoPanTiltControl;
            if (eneoPanTiltControl == null)
            {
                return;
            }

            await Task.Run(() =>
            {
                //Disable limits
                eneoPanTiltControl.DisableLimit();

                //Move to zero position
                this._panTiltControl.PanTiltAbsolute(0, 0);
                this._positionChecker.ComparePosition(new PanTiltPosition(0, 0));

                //PanMin
                this._panTiltControl.PanRelative(-30);
                this._positionChecker.ComparePosition(new PanTiltPosition(limits.PanMin - 10, 0), tolerance: 5, timeout: 50, retry: 200);
                eneoPanTiltControl.SetLimitLeft();
                this._panTiltControl.PanAbsolute(limits.PanMin);
                this._positionChecker.ComparePosition(new PanTiltPosition(limits.PanMin, 0));
                eneoPanTiltControl.SetLimitLeft();

                //PanMax
                this._panTiltControl.PanRelative(30);
                this._positionChecker.ComparePosition(new PanTiltPosition(limits.PanMax + 10, 0), tolerance: 5, timeout: 50, retry: 200);
                eneoPanTiltControl.SetLimitRigth();
                this._panTiltControl.PanAbsolute(limits.PanMax);
                this._positionChecker.ComparePosition(new PanTiltPosition(limits.PanMax, 0));
                eneoPanTiltControl.SetLimitRigth();

                //Move to zero position
                this._panTiltControl.PanTiltAbsolute(0, 0);
                this._positionChecker.ComparePosition(new PanTiltPosition(0, 0));

                //TiltMin
                this._panTiltControl.TiltRelative(-20);
                this._positionChecker.ComparePosition(new PanTiltPosition(0, limits.TiltMin - 10), tolerance: 5, timeout: 50, retry: 200);
                eneoPanTiltControl.SetLimitDown();
                this._panTiltControl.TiltAbsolute(limits.TiltMin);
                this._positionChecker.ComparePosition(new PanTiltPosition(0, limits.TiltMin));
                eneoPanTiltControl.SetLimitDown();

                //TiltMax
                this._panTiltControl.TiltRelative(20);
                this._positionChecker.ComparePosition(new PanTiltPosition(0, limits.TiltMax + 10), tolerance: 5, timeout: 50, retry: 200);
                eneoPanTiltControl.SetLimitUp();
                this._panTiltControl.TiltAbsolute(limits.TiltMax);
                this._positionChecker.ComparePosition(new PanTiltPosition(0, limits.TiltMax));
                eneoPanTiltControl.SetLimitUp();

                //Enable limits
                eneoPanTiltControl.EnableLimit();

                //Move to zero position
                this._panTiltControl.PanTiltAbsolute(0, 0);
                this._positionChecker.ComparePosition(new PanTiltPosition(0, 0));
            });
        }

        private async void buttonSetLimits_Click(object sender, EventArgs e)
        {
            double.TryParse(this.textBoxLimitLeft.Text, out var panMin);
            double.TryParse(this.textBoxLimitRight.Text, out var panMax);
            double.TryParse(this.textBoxLimitUp.Text, out var tiltMax);
            double.TryParse(this.textBoxLimitDown.Text, out var tiltMin);

            this.buttonSetLimits.Enabled = false;
            await this.SetLimits(new PanTiltLimit { PanMin = panMin, PanMax = panMax, TiltMin = tiltMin, TiltMax = tiltMax });
            this.buttonSetLimits.Enabled = true;

            await this.RefreshLimits();
        }

        #endregion

        #region Smoothing

        private void buttonSmoothingLow_Click(object sender, EventArgs e)
        {
            this.SetSmoothing(50, 25);
        }

        private void buttonSmoothingNormal_Click(object sender, EventArgs e)
        {
            this.SetSmoothing(100, 50);
        }

        private void buttonSmoothingHigh_Click(object sender, EventArgs e)
        {
            this.SetSmoothing(200, 100);
        }

        private void SetSmoothing(byte acceleration, byte gain)
        {
            this.labelAccleration.Text = $"Acceleration: {acceleration}";
            this.labelGain.Text = $"Gain: {gain}";

            var eneoPanTiltControl = this._panTiltControl as EneoPanTiltControl;
            if (eneoPanTiltControl != null)
            {
                eneoPanTiltControl.SetSmoothing(acceleration, gain);
            }
        }

        #endregion

        #region Other

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
                this._cameraDrawEngine.Clear();
                this._cameraDrawEngine.DrawPtHeadLimits(this._panTiltControl.GetLimits());
                this._cameraDrawEngine.DrawCrossHair(position, Brushes.Black);
                this.UpdateCurrentImage();
            }
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
    }
}
