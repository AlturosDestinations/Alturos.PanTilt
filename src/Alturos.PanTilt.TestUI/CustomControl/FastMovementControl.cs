using Alturos.PanTilt.Diagnostic;
using Alturos.PanTilt.TestUI.Extension;
using Alturos.PanTilt.TestUI.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Alturos.PanTilt.TestUI.CustomControl
{
    public partial class FastMovementControl : UserControl
    {
        private IPanTiltControl _panTiltControl;
        private IPositionChecker _positionChecker;
        private PanTiltPosition _currentPosition;
        private DrawEngine _drawEngine;
        private PanTiltLimit _ptLimit;
        private PanTiltPosition _lastPosition;
        private AutoResetEvent _waitMovementResetEvent = new AutoResetEvent(false);
        private int _currentPtIndices;
        private List<PanTiltPosition> _panTiltPositions;
        private List<FastMovementInfo> _movementInfos;
        private BindingSource _bindingSource = new BindingSource();
        private object _syncLock = new object();
        private Timer _ptUpdateTimer;
        private int _drawEngineMultiplier = 4;
        private object _syncObject = new object();

        public FastMovementControl()
        {
            this.InitializeComponent();
            this._drawEngine = new DrawEngine(this._drawEngineMultiplier);

            this.label1.Text = string.Empty;
            this.labelError.Text = string.Empty;

            this.PreparePositions();
            this._currentPtIndices = 0;

            this._movementInfos = new List<FastMovementInfo>();

            this._bindingSource.DataSource = this._movementInfos;
            this.dataGridViewFastMovement.AutoGenerateColumns = false;
            this.dataGridViewFastMovement.DataSource = this._bindingSource;

            this._ptUpdateTimer = new Timer();
            this._ptUpdateTimer.Elapsed += UpdateTimerElapsed;
            this._ptUpdateTimer.Interval = 250;
        }

        private void PreparePositions()
        {
            this._panTiltPositions = new List<PanTiltPosition>();
            this._panTiltPositions.Add(new PanTiltPosition(0, 0));
            this._panTiltPositions.Add(new PanTiltPosition(50, 30));
            this._panTiltPositions.Add(new PanTiltPosition(-50, -30));
        }

        private void UpdateTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (this._panTiltControl == null || this.buttonStartFast.Enabled)
                {
                    return;
                }

                var currentPosition = this._currentPosition;
                if (currentPosition == null)
                {
                    return;
                }

                var info = new FastMovementInfo(DateTime.Now, this._currentPtIndices, "CurrentPosition", currentPosition.Pan, currentPosition.Tilt);
                this.AddMovementInfo(info);

                if (this._lastPosition != null)
                {
                    lock (this._syncObject)
                    {
                        this._drawEngine.DrawLine(currentPosition, this._lastPosition, new Pen(Brushes.HotPink).Color, 4);
                        this._drawEngine.DrawCircle(new PanTiltPosition(currentPosition.Pan, currentPosition.Tilt), 5, Brushes.HotPink);
                    }
                    this.UpdateCurrentImage();
                }

                this._lastPosition = currentPosition;

                this.RefreshGrid();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error", exception.Message);
            }
        }

        public void SetPanTiltControl(IPanTiltControl panTiltControl)
        {
            this._panTiltControl = panTiltControl;
            this._positionChecker = new PositionChecker(this._panTiltControl);

            this._panTiltControl.PositionChanged += PanTiltPositionChanged;
            this._panTiltControl.LimitChanged += PanTiltLimitChanged;

            this.CheckPtLimitAsync().GetAwaiter().GetResult();
        }

        private void PanTiltLimitChanged()
        {
            this.CheckPtLimitAsync().GetAwaiter().GetResult();
        }

        private async Task CheckPtLimitAsync()
        {
            if (this._panTiltControl == null)
            {
                return;
            }

            await Task.Delay(1000).ConfigureAwait(false);
            this._ptLimit = this._panTiltControl.GetLimits();
            this.PrepareDrawing();
        }

        public void AddMovementInfo(FastMovementInfo item)
        {
            lock (this._syncLock)
            {
                this._movementInfos.Insert(0, item);
            }
        }

        private void RefreshGrid()
        {
            this.dataGridViewFastMovement.Invoke(o => this._bindingSource.ResetBindings(false));
        }

        private void PanTiltPositionChanged(PanTiltPosition position)
        {
            this._currentPosition = position;
        }

        private void UpdateCurrentImage()
        {
            var oldImage = this.pictureBox1.Image;
            lock (this._syncObject)
            {
                this.pictureBox1.Image = this._drawEngine.GetImage();
            }
            oldImage?.Dispose();
        }

        //private void SaveRectangleTestImageToFile(double degrees, int seconds)
        //{
        //    var deviationOverrunText = this._deviationOverrunDetected ? "Fail" : "Success";
        //    var fileName = $"MovementTest_{DateTime.Now:yyyy-MM-dd_hh-mm-ss}_D{degrees}_S{seconds}_PT{this._ptLimit}_{deviationOverrunText}.jpg";
        //    var dirName = "RectangleMovementTestImages";
        //    this.SaveTestImageToFile(dirName, fileName);
        //}

        //private void SaveTestImageToFile(string dirName, string fileName)
        //{
        //    if (!Directory.Exists(dirName))
        //    {
        //        Directory.CreateDirectory(dirName);
        //    }

        //    var directory = Path.Combine(dirName, fileName);
        //    var format = ImageFormat.Jpeg;

        //    using (var bitmap = this._drawEngine.GetImage())
        //    {
        //        bitmap.Save(directory, format);
        //    }
        //}

        private async void buttonStartFast_Click(object sender, EventArgs e)
        {
            this.buttonStartFast.Enabled = false;
            await this.RunTest();
            this.buttonStartFast.Enabled = true;
        }

        private async Task RunTest()
        {
            this._movementInfos.Clear();
            this.PrepareDrawing();
            this._lastPosition = null;
            this._currentPtIndices = -1;
            this._ptUpdateTimer.Enabled = true;
            this._ptUpdateTimer.Start();

            var timeoutCommands = (int)this.numericUpDownTime.Value;

            foreach (var panTiltPosition in this._panTiltPositions)
            {
                this._currentPtIndices++;

                if (this._currentPtIndices == 0)
                {
                    this.DrawMovement(this._panTiltPositions.First(), this._panTiltPositions.First(), this._currentPtIndices, false);
                }
                else
                {
                    var isLast = this._currentPtIndices == this._panTiltPositions.Count - 1;
                    this.DrawMovement(this._panTiltPositions[_currentPtIndices - 1], this._panTiltPositions[_currentPtIndices], this._currentPtIndices, isLast);
                }

                var info = new FastMovementInfo(DateTime.Now, this._currentPtIndices, "NewPosition", panTiltPosition.Pan, panTiltPosition.Tilt);
                this.AddMovementInfo(info);

                await Task.Run(() =>
                {
                    this._panTiltControl?.PanTiltAbsolute(panTiltPosition.Pan, panTiltPosition.Tilt);
                    this._waitMovementResetEvent.WaitOne(timeoutCommands);
                });
            }

            await Task.Run(() =>
            {
                this._positionChecker?.ComparePosition(this._panTiltPositions.Last());
            });

            this._ptUpdateTimer.Stop();
            this._ptUpdateTimer.Enabled = false;

            this.RefreshGrid();
        }

        private void DrawMovement(PanTiltPosition start, PanTiltPosition end, int number, bool isLast)
        {
            var brush = Brushes.DarkGreen;
            var dimension = isLast ? 50 : 25;

            lock (this._syncObject)
            {
                this._drawEngine.DrawLineWithArrow(start, end, new Pen(brush).Color, 4);
                this._drawEngine.DrawText(new PanTiltPosition(end.Pan - 10, end.Tilt), number.ToString(), brush, true);
                this._drawEngine.DrawCircle(new PanTiltPosition(end.Pan, end.Tilt), 5, brush);
                this._drawEngine.DrawCircle(new PanTiltPosition(end.Pan - 10, end.Tilt), dimension, brush);
            }

            this.UpdateCurrentImage();
        }

        private void PrepareDrawing()
        {
            this._drawEngine.Clear();
            this._drawEngine.DrawPtHeadLimits(this._ptLimit);
            this._drawEngine.DrawText(new PanTiltPosition(-180, 90), "Requested Movement", Brushes.DarkGreen, false);
            this._drawEngine.DrawText(new PanTiltPosition(-180, 80), "Real Movement", Brushes.HotPink, false);
            this.UpdateCurrentImage();
        }

        private void dataGridViewFastMovement_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null)
            {
                return;
            }

            var item = grid.Rows[e.RowIndex]?.DataBoundItem as FastMovementInfo;
            if (item == null)
            {
                return;
            }

            if (item.Type != "NewPosition")
            {
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                return;
            }

            grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkGreen;
        }
    }
}
