using Alturos.PanTilt.Manufacturer.Alturos.Eprom;
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
        private MainConfig _config;

        public AlturosUserControl()
        {
            this.InitializeComponent();
            this.labelUpdateStatus.Text = string.Empty;
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
            this.progressBar1.Invoke((MethodInvoker)delegate { this.progressBar1.Value = 0; });

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

        private async void buttonGetConfig_Click(object sender, EventArgs e)
        {
            this._config = await this._panTiltControl.GetConfigAsync();
            this.ViewIpAddress(this._config.Ip);


            this.propertyGrid1.SelectedObject = this._config;
        }

        private async void buttonSetConfig_Click(object sender, EventArgs e)
        {
            this._config = (MainConfig)this.propertyGrid1.SelectedObject;
            var ipAddress = this.GetIpAddress();
            if (ipAddress.HasValue)
            {
                this._config.Ip = ipAddress.Value;
            }

            await this._panTiltControl.SetConfigAsync(this._config);
        }

        private void ViewIpAddress(IpAddressData ipAddressData)
        {
            this.textBoxIpAddressPart1.Text = ipAddressData.Part1.ToString();
            this.textBoxIpAddressPart2.Text = ipAddressData.Part2.ToString();
            this.textBoxIpAddressPart3.Text = ipAddressData.Part3.ToString();
            this.textBoxIpAddressPart4.Text = ipAddressData.Part4.ToString();
        }

        private IpAddressData? GetIpAddress()
        {
            if (!byte.TryParse(this.textBoxIpAddressPart1.Text, out var ipPart1))
            {
                return null;
            }
            if (!byte.TryParse(this.textBoxIpAddressPart2.Text, out var ipPart2))
            {
                return null;
            }
            if (!byte.TryParse(this.textBoxIpAddressPart3.Text, out var ipPart3))
            {
                return null;
            }
            if (!byte.TryParse(this.textBoxIpAddressPart4.Text, out var ipPart4))
            {
                return null;
            }

            return new IpAddressData { Part1 = ipPart1, Part2 = ipPart2, Part3 = ipPart3, Part4 = ipPart4 };
        }
    }
}
