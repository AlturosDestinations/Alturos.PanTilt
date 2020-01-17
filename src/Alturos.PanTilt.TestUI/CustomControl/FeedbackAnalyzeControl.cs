using Alturos.PanTilt.TestUI.Extension;
using Alturos.PanTilt.TestUI.Model;
using Alturos.PanTilt.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Alturos.PanTilt.TestUI.CustomControl
{
    public partial class FeedbackAnalyzeControl : UserControl
    {
        private IPanTiltControl _panTiltControl;
        private IPositionChecker _positionChecker;
        private PanTiltPosition _currentPosition;

        public FeedbackAnalyzeControl()
        {
            this.InitializeComponent();
            this._currentPosition = new PanTiltPosition();
            this.labelInfo.Text = string.Empty;
        }

        public void SetPanTiltControl(IPanTiltControl panTiltControl)
        {
            this._panTiltControl = panTiltControl;
            this._panTiltControl.PositionChanged += PositionChanged;
            this._positionChecker = new PositionChecker(this._panTiltControl);
        }

        private void PositionChanged(PanTiltPosition position)
        {
            this._currentPosition = position;
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            var startPosition = new PanTiltPosition(-60, -40);

            this.buttonStart.Enabled = false;
            this.labelInfo.Text = $"Drive to test start position {startPosition}";

            this._panTiltControl.PanTiltAbsolute(startPosition);
            await this._positionChecker.ComparePositionAsync(startPosition, retry: 50);

            var chartPanMaximum = 5;
            var chartTiltMaximum = 5;
            var currentPanSpeed = 0.0;
            var currentTiltSpeed = 0.0;

            var cancellationTokenSource = new CancellationTokenSource();

            this.labelInfo.Text = "Start test";

            var feebackItems = new List<FeedbackResult>();

            _ = Task.Run(async () =>
            {
                var lastPosition = new PanTiltPosition();
                lastPosition.Pan = this._currentPosition.Pan;
                lastPosition.Tilt = this._currentPosition.Tilt;

                var delay = 200;
                var multiplier = 1000 / delay;

                while (true)
                {
                    if (cancellationTokenSource.IsCancellationRequested)
                    {
                        return;
                    }

                    var calculatedPan = Math.Abs((lastPosition.Pan - this._currentPosition.Pan) * multiplier);
                    var calculatedTilt = Math.Abs((lastPosition.Tilt - this._currentPosition.Tilt) * multiplier);

                    lastPosition.Pan = this._currentPosition.Pan;
                    lastPosition.Tilt = this._currentPosition.Tilt;

                    feebackItems.Add(new FeedbackResult
                    {
                        Timestamp = DateTime.Now,
                        PanPosition = this._currentPosition.Pan,
                        TiltPosition = this._currentPosition.Tilt,
                        PanSpeedCalculated = calculatedPan,
                        PanSpeedCommand = currentPanSpeed,
                        TiltSpeedCalculated = calculatedTilt,
                        TiltSpeedCommand = currentTiltSpeed
                    });

                    void UpdateChart(Series series, double value)
                    {
                        series.Points.AddY(value);
                        if (series.Points.Count > 20)
                        {
                            series.Points.RemoveAt(0);
                        }
                    }

                    this.chartAnalyze.Invoke(o =>
                    {
                        this.chartAnalyze.ChartAreas[o.Series[0].ChartArea].AxisY.Maximum = chartPanMaximum;
                        this.chartAnalyze.ChartAreas[o.Series[2].ChartArea].AxisY.Maximum = chartTiltMaximum;

                        UpdateChart(o.Series[0], calculatedPan);
                        UpdateChart(o.Series[1], currentPanSpeed);
                        UpdateChart(o.Series[2], calculatedTilt);
                        UpdateChart(o.Series[3], currentTiltSpeed);
                    });

                    await Task.Delay(delay);
                }
            }, cancellationTokenSource.Token);

            var taskPan = Task.Run(async () =>
            {
                var delay = 50;

                for (var i = 0.0; i < 20; i += 0.10)
                {
                    this._panTiltControl.PanRelative(i);
                    chartPanMaximum = (int)Math.Ceiling(i * 1.2);
                    currentPanSpeed = i;
                    await Task.Delay(delay);
                }

                this._panTiltControl.PanRelative(0);
            });

            var taskTilt = Task.Run(async () =>
            {
                var delay = 50;

                for (var i = 0.0; i < 10; i += 0.05)
                {
                    this._panTiltControl.TiltRelative(i);
                    chartTiltMaximum = (int)Math.Ceiling(i * 1.2);
                    currentTiltSpeed = i;

                    await Task.Delay(delay);
                }

                this._panTiltControl.TiltRelative(0);
            });

            this.labelInfo.Text = "Test is running...";

            await Task.WhenAll(taskPan, taskTilt);

            this._panTiltControl.StopMoving();

            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();

            this.dataGridView1.DataSource = feebackItems;

            this.buttonStart.Enabled = true;
            this.labelInfo.Text = string.Empty;
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var item = this.dataGridView1.Rows[e.RowIndex].DataBoundItem as FeedbackResult;
            if (item.PanSpeedDifference > 5 || item.TiltSpeedDifference > 5)
            {
                this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.OrangeRed;
            }

            if (item.TiltSpeedCalculated < 0.001 || item.PanSpeedCalculated < 0.001)
            {
                this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
            }
        }
    }
}
