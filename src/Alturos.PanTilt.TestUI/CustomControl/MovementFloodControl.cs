using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alturos.PanTilt.TestUI.Extension;
using Alturos.PanTilt.Tools;

namespace Alturos.PanTilt.TestUI.CustomControl
{
    public partial class MovementFloodControl : UserControl
    {
        private IPanTiltControl _panTiltControl;
        private IPositionChecker _positionChecker;

        public MovementFloodControl()
        {
            this.InitializeComponent();
        }

        public void SetPanTiltControl(IPanTiltControl panTiltControl)
        {
            this._panTiltControl = panTiltControl;
            this._positionChecker = new PositionChecker(this._panTiltControl);
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            this.buttonStart.Enabled = false;
            this.textBoxHistory.Text = string.Empty;

            double.TryParse(this.textBoxPan.Text, out var pan);
            double.TryParse(this.textBoxSpeed.Text, out var degreePerSecond);

            this.textBoxHistory.Invoke(o => o.Text += $"Drive to start position {pan}, please wait...\r\n");

            this._panTiltControl.PanTiltAbsolute(pan, 0);
            await this._positionChecker.ComparePositionAsync(new PanTiltPosition(pan, 0), retry: 20);

            this.textBoxHistory.Invoke(o => o.Text += $"Start position reached\r\n");
            await Task.Delay(100);

            for (var delay = 0; delay < 200; delay += 10)
            {
                this.textBoxHistory.Invoke(o => o.Text += $"Start test case with {delay}ms delay\r\n");

                for (var i = 0; i < 10; i++)
                {
                    this._panTiltControl.PanRelative(degreePerSecond);
                    this.textBoxHistory.Invoke(o => o.Text += $"{DateTime.Now:HH:mm:ss.fff} PanRelative {degreePerSecond}\r\n");
                    await Task.Delay(delay);
                }

                for (var i = 0; i < 10; i++)
                {
                    this._panTiltControl.PanRelative(-degreePerSecond);
                    this.textBoxHistory.Invoke(o => o.Text += $"{DateTime.Now:HH:mm:ss.fff} PanRelative -{degreePerSecond}\r\n");
                    await Task.Delay(delay);
                }
            }

            this._panTiltControl.StopMoving();

            this.buttonStart.Enabled = true;
        }
    }
}
