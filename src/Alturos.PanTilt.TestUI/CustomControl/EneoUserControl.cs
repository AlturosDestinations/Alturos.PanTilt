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
            this.SetSmootingValues("50", "25");
        }

        public void SetPanTiltControl(IPanTiltControl panTiltControl)
        {
            this._panTiltControl = panTiltControl;
        }

        private void buttonSmoothingLow_Click(object sender, EventArgs e)
        {
            this.SetSmootingValues("50", "25");
        }

        private void buttonSmoothingNormal_Click(object sender, EventArgs e)
        {
            this.SetSmootingValues("100", "50");
        }

        private void buttonSmoothingHigh_Click(object sender, EventArgs e)
        {
            this.SetSmootingValues("200", "100");
        }

        private void SetSmootingValues(string acceleration, string gain)
        {
            this.textBoxAcceleration.Text = acceleration;
            this.textBoxGain.Text = gain;
        }

        private void buttonSetSmoothing_Click(object sender, EventArgs e)
        {
            byte.TryParse(this.textBoxAcceleration.Text, out var acceleration);
            byte.TryParse(this.textBoxGain.Text, out var gain);

            var eneoPanTiltControl = this._panTiltControl as EneoPanTiltControl;
            if (eneoPanTiltControl != null)
            {
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
