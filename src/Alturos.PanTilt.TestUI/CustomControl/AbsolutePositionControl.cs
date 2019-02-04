using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alturos.PanTilt.TestUI.Extension;

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
            //this._positionChecker = new PositionChecker(this._panTiltControl);

            //this._panTiltControl.PositionChanged += PanTiltPositionChanged;
            //this._panTiltControl.LimitOverrun += PanTiltLimitOverrun;
            //this._panTiltControl.LimitChanged += PanTiltLimitChanged;

            //this.CheckPtLimitAsync().GetAwaiter().GetResult();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Task.Run(() => this.TestLogic());
        }

        private void TestLogic()
        {
            var limits = this._panTiltControl.GetLimits();
            var positionChecker = new PositionChecker(this._panTiltControl);

            this._panTiltControl.TiltAbsolute(0);
            for (var pan = limits.PanMin; pan < limits.PanMax; pan += 0.05)
            {
                this._panTiltControl.PanAbsolute(pan);
                if (!positionChecker.ComparePosition(new PanTiltPosition(pan, 0), 0.1, 5, 2000))
                {
                    var position = this._panTiltControl.GetPosition();
                    this.textBoxResult.Invoke(o => o.Text += $"Failure move to pan position {pan} ({position.Pan})\r\n");
                }
            }

            this._panTiltControl.PanAbsolute(0);
            for (var tilt = limits.TiltMin; tilt < limits.TiltMax; tilt += 0.05)
            {
                this._panTiltControl.TiltAbsolute(tilt);
                if (!positionChecker.ComparePosition(new PanTiltPosition(0, tilt), 0.1, 5, 2000))
                {
                    var position = this._panTiltControl.GetPosition();
                    this.textBoxResult.Invoke(o => o.Text += $"Failure move to tilt position {tilt} ({position.Tilt})\r\n");
                }
            }
        }
    }
}
