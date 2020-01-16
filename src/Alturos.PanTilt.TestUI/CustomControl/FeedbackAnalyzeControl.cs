using Alturos.PanTilt.TestUI.Extension;
using Alturos.PanTilt.Tools;
using System;
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

            this._panTiltControl.PanTiltAbsolute(startPosition);
            await this._positionChecker.ComparePositionAsync(startPosition);

            var chartPanMaximum = 5;
            var chartTiltMaximum = 5;
            var currentPanSpeed = 0.0;
            var currentTiltSpeed = 0.0;

            var cancellationTokenSource = new CancellationTokenSource();

            _ = Task.Run(async () =>
            {
                var lastPosition = new PanTiltPosition();
                var delay = 100;
                var multiplier = 1000 / delay;

                while (true)
                {
                    if (cancellationTokenSource.IsCancellationRequested)
                    {
                        return;
                    }

                    var differencePan = Math.Abs((lastPosition.Pan - this._currentPosition.Pan) * multiplier);
                    var differenceTilt = Math.Abs((lastPosition.Tilt - this._currentPosition.Tilt) * multiplier);

                    lastPosition.Pan = this._currentPosition.Pan;
                    lastPosition.Tilt = this._currentPosition.Tilt;

                    void UpdateChart(Series series, double value)
                    {
                        series.Points.AddY(value);
                        if (series.Points.Count > 20)
                        {
                            series.Points.RemoveAt(0);
                        }
                    }

                    this.chart1.Invoke(o =>
                    {
                        this.chart1.ChartAreas[o.Series[0].ChartArea].AxisY.Maximum = chartPanMaximum;
                        this.chart1.ChartAreas[o.Series[2].ChartArea].AxisY.Maximum = chartTiltMaximum;

                        UpdateChart(o.Series[0], differencePan);
                        UpdateChart(o.Series[1], currentPanSpeed);
                        UpdateChart(o.Series[2], differenceTilt);
                        UpdateChart(o.Series[3], currentTiltSpeed);
                    });

                    await Task.Delay(delay);
                }
            }, cancellationTokenSource.Token);

            var task1 = Task.Run(async () =>
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

            var task2 = Task.Run(async () =>
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

            await Task.WhenAll(task1, task2);

            this._panTiltControl.StopMoving();

            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
