using Alturos.PanTilt.Diagnostic;
using Alturos.PanTilt.TestUI.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alturos.PanTilt.TestUI.CustomControl
{
    public partial class ContiniousMovementControl : UserControl
    {
        private IPanTiltControl _panTiltControl;
        private PanTiltPosition _currentPosition;
        private DrawEngine _drawEngine;
        private PanTiltLimit _ptLimit;
        private bool _limitOverrunDetected;
        private bool _deviationOverrunDetected;
        private PanTiltPosition _lastPosition;
        private bool _stoppedManually;
        private AutoResetEvent _waitMovementResetEvent = new AutoResetEvent(false);
        private int _multiplier = 4;

        public ContiniousMovementControl()
        {
            this.InitializeComponent();
            this._drawEngine = new DrawEngine(this._multiplier);

            this.labelPanTiltPosition.Text = string.Empty;
            this.labelError.Text = string.Empty;

            this.buttonStart.Enabled = true;
            this.buttonStartRelative.Enabled = true;
            this.buttonStop.Enabled = false;

            this.UpdatePtLimit();
        }

        public void SetPanTiltControl(IPanTiltControl panTiltControl)
        {
            this._panTiltControl = panTiltControl;
            this._panTiltControl.PositionChanged += PanTiltPositionChanged;
            this._panTiltControl.LimitOverrun += PanTiltLimitOverrun;
            this._panTiltControl.LimitChanged += PanTiltLimitChanged;

            this.UpdatePtLimit();
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            this.buttonStart.Enabled = false;
            this.buttonStartRelative.Enabled = false;
            this.buttonStop.Enabled = true;
            this._stoppedManually = false;

            var reverse = this.checkBoxReverse.Checked;

            if (this.checkBoxAutostart.Checked)
            {
                await this.MoveCircle(10, 3, reverse);
                await this.MoveCircle(10, 6, reverse);
                await this.MoveCircle(20, 6, reverse);
                await this.MoveCircle(20, 9, reverse);
                await this.MoveCircle(30, 9, reverse);
                await this.MoveCircle(30, 12, reverse);
                await this.MoveCircle(40, 12, reverse);
                await this.MoveCircle(40, 15, reverse);
            }
            else
            {
                int detailLevel = (int)this.numericUpDownPTDetailLevel.Value;
                int radius = (int)this.numericUpDownRadius.Value;

                await this.MoveCircle(radius, detailLevel, reverse);
            }

            this.buttonStart.Enabled = true;
            this.buttonStartRelative.Enabled = true;
            this.buttonStop.Enabled = false;
        }

        private async void buttonStartRelative_Click(object sender, EventArgs e)
        {
            this._stoppedManually = false;
            this.buttonStart.Enabled = false;
            this.buttonStartRelative.Enabled = false;
            this.buttonStop.Enabled = true;

            if (this.checkBoxRelativeLoop.Checked)
            {
                await this.MoveRectangleRelative(2, 15);
                await this.MoveRectangleRelative(5, 10);
                await this.MoveRectangleRelative(7, 7);
                await this.MoveRectangleRelative(10, 4);
            }
            else
            {
                var degreePerSeconds = (int)this.numericUpDownDegrees.Value;
                var seconds = (int)this.numericUpDownSeconds.Value;

                await this.MoveRectangleRelative(degreePerSeconds, seconds);
            }

            this.buttonStart.Enabled = true;
            this.buttonStartRelative.Enabled = true;
            this.buttonStop.Enabled = false;
        }

        private void checkBoxAutostart_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDownPTDetailLevel.Enabled = !this.checkBoxAutostart.Checked;
            this.numericUpDownRadius.Enabled = !this.checkBoxAutostart.Checked;
        }

        private void checkBoxRelativeLoop_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDownDegrees.Enabled = !this.checkBoxRelativeLoop.Checked;
            this.numericUpDownSeconds.Enabled = !this.checkBoxRelativeLoop.Checked;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this._waitMovementResetEvent.Set();
            this._stoppedManually = true;

            this.buttonStart.Enabled = true;
            this.buttonStartRelative.Enabled = true;
            this.buttonStop.Enabled = false;
        }

        private async Task MoveCircle(int radius, int detailLevel = 8, bool reverse = false)
        {
            if (this._stoppedManually)
            {
                return;
            }

            this.PrepareDrawing();

            await Task.Run(() =>
            {
                this._panTiltControl.PanTiltAbsolute(0, 0);
                this._waitMovementResetEvent.WaitOne(1000);
                this._panTiltControl.ComparePosition(new PanTiltPosition(0, 0), 0.2, 20, 40);
            });

            var circleLogic = new CircleLogic();

            this._currentPosition = new PanTiltPosition(0, 0);
            this._deviationOverrunDetected = false;

            var items = new List<PanTiltPosition>();
            for (var r = radius; r <= 180; r += radius)
            {
                var positions = circleLogic.CalculatePtPositions(r, detailLevel);

                if (reverse)
                {
                    positions.Reverse();
                }

                items.AddRange(positions);
            }

            this._lastPosition = null;

            for (var i = 1; i < items.Count; i++)
            {
                if (this._stoppedManually)
                {
                    return;
                }

                var previousPt = items[i - 1];
                var currentPt = items[i];

                this._drawEngine.DrawLine(previousPt, currentPt, Color.DarkGreen, 4);

                if (this._lastPosition != null)
                {
                    this._drawEngine.DrawLine(this._lastPosition, _currentPosition, Color.HotPink, 2);
                }

                this._lastPosition = this._currentPosition;

                this.labelPanTiltPosition.Text = $"Pan:{currentPt.Pan:0.00} Tilt:{currentPt.Tilt:0.00}";
                this.UpdateCurrentImage();

                await Task.Run(() =>
                {
                    this._panTiltControl.PanTiltAbsolute(currentPt.Pan, currentPt.Tilt);
                    this._panTiltControl.ComparePosition(new PanTiltPosition(currentPt.Pan, currentPt.Tilt), 0.1, 50, 40);
                });
            }

            this.UpdateCurrentImage();
            this.SaveCircleTestImageToFile(radius, detailLevel);
        }

        private void PanTiltLimitChanged()
        {
            this.UpdatePtLimit();
        }

        private async void UpdatePtLimit()
        {
            if (this._panTiltControl == null)
            {
                return;
            }

            await Task.Delay(1000);
            this._ptLimit = this._panTiltControl.GetLimits();
            this.PrepareDrawing();
        }

        private void PanTiltPositionChanged(PanTiltPosition position)
        {
            this._currentPosition = position;
        }

        private void PanTiltLimitOverrun()
        {
            this.labelError.Invoke(o => o.Text = $"{DateTime.Now.ToShortTimeString()} - LimitOverrun detected");
            this._limitOverrunDetected = true;
        }

        private async Task MoveRectangleRelative(double degreePerSeconds, int seconds)
        {
            if (this._stoppedManually)
            {
                return;
            }

            this.PrepareDrawing();

            await Task.Run(() =>
            {
                this._panTiltControl.PanTiltAbsolute(0, 0);
                this._waitMovementResetEvent.WaitOne(1000);
                this._panTiltControl.ComparePosition(new PanTiltPosition(0, 0), 0.1, 20, 40);
            });

            this._currentPosition = _panTiltControl.GetPosition();
            this._deviationOverrunDetected = false;

            var items = new List<PanTiltPosition>();
            items.Add(new PanTiltPosition(0, 0));
            items.Add(new PanTiltPosition(0, degreePerSeconds / 2));
            items.Add(new PanTiltPosition(degreePerSeconds / 2, 0));
            items.Add(new PanTiltPosition(0, -degreePerSeconds));
            items.Add(new PanTiltPosition(-degreePerSeconds, 0));
            items.Add(new PanTiltPosition(0, degreePerSeconds));
            items.Add(new PanTiltPosition(degreePerSeconds / 2, 0));

            var absoluteCurrentPt = _currentPosition;
            var absolutePreviousPt = _currentPosition;
            var allowedDeriviation = 0.5 * degreePerSeconds;

            this._lastPosition = null;
            int i = 1;

            while (!_stoppedManually && i < items.Count)
            {
                var relativePreviousPt = items[i - 1];
                var relativeCurrentPt = items[i];

                absolutePreviousPt = absolutePreviousPt.AddRelativePosition(relativePreviousPt, seconds);
                absoluteCurrentPt = absoluteCurrentPt.AddRelativePosition(relativeCurrentPt, seconds);
                this._drawEngine.DrawLine(absolutePreviousPt, absoluteCurrentPt, Color.DarkGreen, 4);

                this.labelPanTiltPosition.Text = $"Pan:{_currentPosition.Pan:0.00} Tilt:{_currentPosition.Tilt:0.00}";

                if (_lastPosition != null)
                {
                    this._drawEngine.DrawLine(this._lastPosition, _currentPosition, Color.HotPink, 3);
                }
                this.UpdateCurrentImage();

                this._lastPosition = this._currentPosition;

                await Task.Run(() =>
                {
                    this._panTiltControl.PanTiltRelative(relativeCurrentPt.Pan, relativeCurrentPt.Tilt);
                    this._waitMovementResetEvent.WaitOne(seconds * 1000);
                    this._panTiltControl.StopMoving();
                    this.ProveDeriviation(absoluteCurrentPt, allowedDeriviation);
                });
                i++;
            }

            if (_lastPosition != null)
            {
                this._drawEngine.DrawLine(this._lastPosition, _currentPosition, Color.HotPink, 3);
            }
            this.UpdateCurrentImage();
            this.SaveRectangleTestImageToFile(degreePerSeconds, seconds);
        }

        private void ProveDeriviation(PanTiltPosition absoluteCurrentPt, double allowedDeriviation)
        {
            if (this._stoppedManually)
            {
                return;
            }

            var deviationOk = this._panTiltControl.ComparePosition(absoluteCurrentPt, allowedDeriviation);
            this._deviationOverrunDetected = !this._deviationOverrunDetected && !deviationOk;

            if (this._deviationOverrunDetected)
            {
                this.labelError.Invoke(o => o.Text = $"{DateTime.Now.ToShortTimeString()} - Too high deviation detected");
            }
            else
            {
                this.labelError.Invoke(o => o.Text = String.Empty);
            }
        }

        private void UpdateCurrentImage()
        {
            var oldImage = this.pictureBox1.Image;
            this.pictureBox1.Image = this._drawEngine.GetImage();
            oldImage?.Dispose();
        }

        private void SaveCircleTestImageToFile(int radius, int detailLevel)
        {
            var limitOverrunText = this._limitOverrunDetected ? "Fail" : "Success";
            var fileName = $"MovementTest_{DateTime.Now:yyyy-MM-dd_hh-mm-ss}_R{radius}_D{detailLevel}_PT{this._ptLimit}_{limitOverrunText}.jpg";
            var dirName = "CircleMovementTestImages";
            this.SaveTestImageToFile(dirName, fileName);
        }

        private void SaveRectangleTestImageToFile(double degrees, int seconds)
        {
            var deviationOverrunText = this._deviationOverrunDetected ? "Fail" : "Success";
            var fileName = $"MovementTest_{DateTime.Now:yyyy-MM-dd_hh-mm-ss}_D{degrees}_S{seconds}_PT{this._ptLimit}_{deviationOverrunText}.jpg";
            var dirName = "RectangleMovementTestImages";
            this.SaveTestImageToFile(dirName, fileName);
        }

        private void SaveTestImageToFile(string dirName, string fileName)
        {
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }

            var directory = Path.Combine(dirName, fileName);
            var format = ImageFormat.Jpeg;

            using (var bitmap = this._drawEngine.GetImage())
            {
                bitmap.Save(directory, format);
            }
        }

        private void PrepareDrawing()
        {
            this._drawEngine.Clear();
            this._drawEngine.DrawPtHeadLimits(this._ptLimit);
            this._drawEngine.DrawText(new PanTiltPosition(-180, 90), "Requested Movement", Brushes.DarkGreen, false);
            this._drawEngine.DrawText(new PanTiltPosition(-180, 80), "Real Movement", Brushes.HotPink, false);
            this.UpdateCurrentImage();
        }
    }
}
