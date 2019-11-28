using System;
using System.Windows.Forms;

namespace Alturos.PanTilt.TestUI.CustomControl
{
    public partial class EneoUserControl : UserControl
    {
        private IPanTiltControl _panTiltControl;

        public EneoUserControl()
        {
            this.InitializeComponent();
        }

        public void SetPanTiltControl(IPanTiltControl panTiltControl)
        {
            this._panTiltControl = panTiltControl;
            this.SetSmoothing(100, 50);
        }

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
            var eneoPanTiltControl = this._panTiltControl as EneoPanTiltControl;
            if (eneoPanTiltControl != null)
            {
                this.labelAccleration.Text = $"Acceleration: {acceleration}";
                this.labelGain.Text = $"Gain: {gain}";

                eneoPanTiltControl.SetSmoothing(acceleration, gain);
            }
        }

        private void buttonLimitUp_Click(object sender, EventArgs e)
        {
            if (this._panTiltControl is EneoPanTiltControl eneoPanTiltControl)
            {
                eneoPanTiltControl.SetLimitUp();
            }
        }

        private void buttonSetLimitDown_Click(object sender, EventArgs e)
        {
            if (this._panTiltControl is EneoPanTiltControl eneoPanTiltControl)
            {
                eneoPanTiltControl.SetLimitDown();
            }
        }

        private void buttonLimitLeft_Click(object sender, EventArgs e)
        {
            if (this._panTiltControl is EneoPanTiltControl eneoPanTiltControl)
            {
                eneoPanTiltControl.SetLimitLeft();
            }
        }

        private void buttonSetLimitRight_Click(object sender, EventArgs e)
        {
            if (this._panTiltControl is EneoPanTiltControl eneoPanTiltControl)
            {
                eneoPanTiltControl.SetLimitRigth();
            }
        }

        private void buttonEnableLimits_Click(object sender, EventArgs e)
        {
            if (this._panTiltControl is EneoPanTiltControl eneoPanTiltControl)
            {
                eneoPanTiltControl.EnableLimit();
            }
        }

        private void buttonDisableLimits_Click(object sender, EventArgs e)
        {
            if (this._panTiltControl is EneoPanTiltControl eneoPanTiltControl)
            {
                eneoPanTiltControl.DisableLimit();
            }
        }
    }
}
