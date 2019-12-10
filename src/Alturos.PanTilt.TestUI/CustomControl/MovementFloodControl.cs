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

            double.TryParse(this.textBoxPan.Text, out var pan);
            double.TryParse(this.textBoxSpeed.Text, out var degreePerSecond);

            this.textBoxHistory.Text = string.Empty;
            this._panTiltControl.PanTiltAbsolute(pan, 0);
            await this._positionChecker.ComparePositionAsync(new PanTiltPosition(pan, 0), retry: 20);

            this._panTiltControl.PanRelative(degreePerSecond);
            this.textBoxHistory.Invoke(o => o.Text += $"{DateTime.Now:HH:mm:ss.fff} PanRelative {degreePerSecond}\r\n");
            await Task.Delay(20);
            this._panTiltControl.PanRelative(degreePerSecond);
            this.textBoxHistory.Invoke(o => o.Text += $"{DateTime.Now:HH:mm:ss.fff} PanRelative {degreePerSecond}\r\n");
            await Task.Delay(20);
            this._panTiltControl.PanRelative(degreePerSecond);
            this.textBoxHistory.Invoke(o => o.Text += $"{DateTime.Now:HH:mm:ss.fff} PanRelative {degreePerSecond}\r\n");
            await Task.Delay(20);
            this._panTiltControl.PanRelative(degreePerSecond);
            this.textBoxHistory.Invoke(o => o.Text += $"{DateTime.Now:HH:mm:ss.fff} PanRelative {degreePerSecond}\r\n");
            await Task.Delay(20);

            this._panTiltControl.PanRelative(-degreePerSecond);
            this.textBoxHistory.Invoke(o => o.Text += $"{DateTime.Now:HH:mm:ss.fff} PanRelative -{degreePerSecond}\r\n");
            await Task.Delay(20);
            this._panTiltControl.PanRelative(-degreePerSecond);
            this.textBoxHistory.Invoke(o => o.Text += $"{DateTime.Now:HH:mm:ss.fff} PanRelative -{degreePerSecond}\r\n");
            await Task.Delay(20);
            this._panTiltControl.PanRelative(-degreePerSecond);
            this.textBoxHistory.Invoke(o => o.Text += $"{DateTime.Now:HH:mm:ss.fff} PanRelative -{degreePerSecond}\r\n");
            await Task.Delay(20);
            this._panTiltControl.PanRelative(-degreePerSecond);
            this.textBoxHistory.Invoke(o => o.Text += $"{DateTime.Now:HH:mm:ss.fff} PanRelative -{degreePerSecond}\r\n");
            await Task.Delay(20);

            this.buttonStart.Enabled = true;
        }
    }
}
