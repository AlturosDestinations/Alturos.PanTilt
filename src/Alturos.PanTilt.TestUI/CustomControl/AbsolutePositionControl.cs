using Alturos.PanTilt.TestUI.Extension;
using Alturos.PanTilt.Tools;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alturos.PanTilt.TestUI.CustomControl
{
    public partial class AbsolutePositionControl : UserControl
    {
        private IPanTiltControl _panTiltControl;

        public AbsolutePositionControl()
        {
            this.InitializeComponent();
        }

        public void SetPanTiltControl(IPanTiltControl panTiltControl)
        {
            this._panTiltControl = panTiltControl;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Task.Run(() => this.TestLogic());
        }

        private void TestLogic()
        {
            var limits = this._panTiltControl.GetLimits();
            if (limits.PanMin == 0 && limits.PanMax == 0 && limits.TiltMin == 0 && limits.TiltMax == 0)
            {
                this.textBoxResult.Invoke(o => o.Text += $"Cannot execute test invalid limits\r\n");
            }

            this.textBoxResult.Invoke(o => o.Text += $"Limits;Pan;{limits.PanMin};{limits.PanMax};Tilt;{limits.TiltMin};{limits.TiltMax}\r\n");
            var positionChecker = new PositionChecker(this._panTiltControl);

            this._panTiltControl.TiltAbsolute(0);
            for (var pan = limits.PanMin; pan < limits.PanMax; pan += 0.05)
            {
                pan = Math.Round(pan, 2);
                this._panTiltControl.PanAbsolute(pan);
                Thread.Sleep(2000);
                if (positionChecker.ComparePosition(new PanTiltPosition(pan, 0), 0.1))
                {
                    var position = this._panTiltControl.GetPosition();
                    this.textBoxResult.Invoke(o => o.Text += $"Good;pan;{pan};{position.Pan}\r\n");
                }
                else
                {
                    var position = this._panTiltControl.GetPosition();
                    this.textBoxResult.Invoke(o => o.Text += $"Failure;pan;{pan};{position.Pan}\r\n");
                }
            }

            this._panTiltControl.PanAbsolute(0);
            for (var tilt = limits.TiltMin; tilt < limits.TiltMax; tilt += 0.05)
            {
                tilt = Math.Round(tilt, 2);
                this._panTiltControl.TiltAbsolute(tilt);
                Thread.Sleep(2000);
                if (positionChecker.ComparePosition(new PanTiltPosition(0, tilt), 0.1))
                {
                    var position = this._panTiltControl.GetPosition();
                    this.textBoxResult.Invoke(o => o.Text += $"Good;tilt;{tilt};{position.Tilt}\r\n");
                }
                else
                {
                    var position = this._panTiltControl.GetPosition();
                    this.textBoxResult.Invoke(o => o.Text += $"Failure;tilt;{tilt};{position.Tilt}\r\n");
                }
            }
        }
    }
}
