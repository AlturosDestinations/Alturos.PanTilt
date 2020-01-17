using Alturos.PanTilt.TestUI.Model;
using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alturos.PanTilt.TestUI.CustomControl
{
    public partial class AlturosUserControl : UserControl
    {
        private AlturosPanTiltControl _panTiltControl;
        private DeviceConfiguration _deviceConfiguration;
        private CancellationTokenSource _cancellationTokenSource;

        public AlturosUserControl()
        {
            this.InitializeComponent();
            this.labelUpdateStatus.Text = "";
        }

        public void SetPanTiltControl(IPanTiltControl panTiltControl)
        {
            this._panTiltControl = panTiltControl as AlturosPanTiltControl;
        }

        public void SetDeviceConfiguration(DeviceConfiguration deviceConfiguration)
        {
            this._deviceConfiguration = deviceConfiguration;
        }

        private async void buttonStartUpdate_Click(object sender, EventArgs e)
        {
            var packageFileName = "pt-head.zip";

            var version = this.textBoxFirmwareVersion.Text.Trim();
            var packageUrl = $"https://skiline.s3-eu-west-1.amazonaws.com/artifacts/pt-head/master-{version}/{packageFileName}";

            this.labelUpdateStatus.Invoke((MethodInvoker)delegate { this.labelUpdateStatus.Text = "download package"; });
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.GetAsync(packageUrl))
            using (var file = File.Create(packageFileName))
            {
                if (!response.IsSuccessStatusCode)
                {
                    this.labelUpdateStatus.Invoke((MethodInvoker)delegate { this.labelUpdateStatus.Text = "download failure"; });
                    return;
                }

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    await stream.CopyToAsync(file);
                }
            }

            this.CleanupTemp();

            this.labelUpdateStatus.Invoke((MethodInvoker)delegate { this.labelUpdateStatus.Text = "extract package"; });
            ZipFile.ExtractToDirectory(packageFileName, "tmp");

            var firmwarePath = @"tmp\pt-head\firmware.bin";

            this.labelUpdateStatus.Invoke((MethodInvoker)delegate { this.labelUpdateStatus.Text = "reinitialize PtHead"; });
            this._panTiltControl.ReinitializePtHead();
            await Task.Delay(400);

            this.labelUpdateStatus.Invoke((MethodInvoker)delegate { this.labelUpdateStatus.Text = "start upload"; });
            this._cancellationTokenSource = new CancellationTokenSource();

            var client = new Tftp.Net.TftpClient(this._deviceConfiguration.PanTiltIpAddress, 69);

            using (var fileStream = File.OpenRead(firmwarePath))
            using (var transfer = client.Upload("firmware.bin"))
            {
                transfer.OnProgress += TransferOnProgress;
                transfer.OnError += TransferOnError;
                transfer.OnFinished += TransferOnFinished;
                transfer.Start(fileStream);

                try
                {
                    await Task.Delay(1000 * 30, this._cancellationTokenSource.Token);
                }
                catch
                {
                    //Ignore
                }

                transfer.OnProgress -= TransferOnProgress;
                transfer.OnError -= TransferOnError;
                transfer.OnFinished -= TransferOnFinished;
            }

            this._cancellationTokenSource.Dispose();
            this.CleanupTemp();
            if (File.Exists(packageFileName))
            {
                File.Delete(packageFileName);
            }

            this.labelUpdateStatus.Invoke((MethodInvoker)delegate { this.labelUpdateStatus.Text = "update done"; });
        }

        private void CleanupTemp()
        {
            if (Directory.Exists("tmp"))
            {
                Directory.Delete("tmp", true);
            }
        }

        private void TransferOnFinished(Tftp.Net.ITftpTransfer transfer)
        {
            this._cancellationTokenSource.Cancel();
        }

        private void TransferOnError(Tftp.Net.ITftpTransfer transfer, Tftp.Net.TftpTransferError error)
        {
            this._cancellationTokenSource.Cancel();
        }

        private void TransferOnProgress(Tftp.Net.ITftpTransfer transfer, Tftp.Net.TftpTransferProgress progress)
        {
            var uploadProgress = (int)Math.Floor(100.0 * progress.TransferredBytes / progress.TotalBytes);
            this.progressBar1.Invoke((MethodInvoker)delegate { this.progressBar1.Value = uploadProgress; });
        }

        private async void buttonTemperature_Click(object sender, EventArgs e)
        {
            var value = await this._panTiltControl.GetTemperatureAsync();
            this.textBoxTemperature.Text = $"{value}°";
        }

        private async void buttonHumidity_Click(object sender, EventArgs e)
        {
            var value = await this._panTiltControl.GetHumidityAsync();
            this.textBoxHumidity.Text = $"{value}%";
        }

        private async void buttonPanHalSensor_Click(object sender, EventArgs e)
        {
            var value = await this._panTiltControl.GetPanHalSensorAsync();
            this.textBoxPanHalSensor.Text = $"{value}";
        }

        private async void buttonTiltHalSensor_Click(object sender, EventArgs e)
        {
            var value = await this._panTiltControl.GetTiltHalSensorAsync();
            this.textBoxTiltHalSensor.Text = $"{value}";
        }

        private async void buttonPanInitialError_Click(object sender, EventArgs e)
        {
            var value = await this._panTiltControl.GetPanInitialErrorAsync();
            this.textBoxPanInitialError.Text = value;
        }

        private async void buttonTiltInitialError_Click(object sender, EventArgs e)
        {
            var value = await this._panTiltControl.GetTiltInitialErrorAsync();
            this.textBoxTiltInitialError.Text = value;
        }
    }
}
