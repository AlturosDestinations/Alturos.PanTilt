using Alturos.PanTilt.TestUI.Model;
using Alturos.PanTilt.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alturos.PanTilt.TestUI.CustomControl
{
    public partial class CommandSequenceControl : UserControl
    {
        private IPanTiltControl _panTiltControl;
        private PositionChecker _positionChecker;
        private List<CommandSequenceResult> _testResults = new List<CommandSequenceResult>();
        private BindingSource _testBindingSource;
        private CancellationTokenSource _cancellationTokenSource;
        private CommandSequence[] _commandSequences;

        public CommandSequenceControl()
        {
            this.InitializeComponent();

            this._testBindingSource = new BindingSource();
            this._testBindingSource.DataSource = this._testResults;

            this.dataGridViewResult.AutoGenerateColumns = false;
            this.dataGridViewResult.DataSource = this._testBindingSource;

            this.buttonAbort.Enabled = false;

            this._commandSequences = new CommandSequence[]
            {
                new CommandSequence("Sequence RPT 1")
                {
                    Steps = new CommandSequenceStep[]
                    {
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(0, 0),
                            WaitPositionIsReached = true
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = 10,
                            TiltSpeed = 10,
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = 10.5,
                            TiltSpeed = 10.5,
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = 15,
                            TiltSpeed = 12,
                        },
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(0, 0),
                            WaitPositionIsReached = true
                        },
                    }
                },
                new CommandSequence("Sequence APT RPT 1")
                {
                    Steps = new CommandSequenceStep[]
                    {
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(0, 0),
                            WaitPositionIsReached = true
                        },
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(-14.25, 10.95),
                            WaitPositionIsReached = true
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = 10,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = 10.2,
                            TiltSpeed = 5.5,
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = 10.5,
                            TiltSpeed = 6,
                        },
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(-14.25, 8.95),
                            WaitPositionIsReached = true
                        },
                    }
                },
                new CommandSequence("Sequence APT RPT 2")
                {
                    Steps = new CommandSequenceStep[]
                    {
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(0, 0),
                            WaitPositionIsReached = true
                        },
                        new CommandSequenceStepAbsolute(150)
                        {
                            Position = new PanTiltPosition(-14.25, 10.95),
                            WaitPositionIsReached = false
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = 10,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = 10.2,
                            TiltSpeed = 5.5,
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = 10.5,
                            TiltSpeed = 6,
                        },
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(-14.25, 8.95),
                            WaitPositionIsReached = true
                        },
                    }
                },
                new CommandSequence("Sequence APT RP 1")
                {
                    Steps = new CommandSequenceStep[]
                    {
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(0, 0),
                            WaitPositionIsReached = true
                        },
                        new CommandSequenceStepAbsolute(500)
                        {
                            Position = new PanTiltPosition(-40, 10),
                            WaitPositionIsReached = false
                        },
                        new CommandSequenceStepAbsolute(200)
                        {
                            Position = new PanTiltPosition(+40, -10),
                            WaitPositionIsReached = false
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = 100,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = -100,
                            TiltSpeed = 5.5,
                        },
                        new CommandSequenceStepRelative(40)
                        {
                            PanSpeed = -100,
                            TiltSpeed = 5.5,
                        },
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(0, 0),
                            WaitPositionIsReached = true
                        },
                    }
                },
                new CommandSequence("Sequence AP RPT 1")
                {
                    Steps = new CommandSequenceStep[]
                    {
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(-30, 0),
                            WaitPositionIsReached = true
                        },
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(-35, 0),
                            WaitPositionIsReached = true
                        },
                        //Drive 5° pan
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        //Drive 10° pan
                        //Drive 0.5s with tilt speed 5.5
                        new CommandSequenceStepRelative(100)
                        {
                            PanSpeed = 20,
                            TiltSpeed = 5.5,
                        },
                        new CommandSequenceStepRelative(100)
                        {
                            PanSpeed = 20,
                            TiltSpeed = 5.5,
                        },
                        new CommandSequenceStepRelative(100)
                        {
                            PanSpeed = 20,
                            TiltSpeed = 5.5,
                        },
                        new CommandSequenceStepRelative(100)
                        {
                            PanSpeed = 20,
                            TiltSpeed = 5.5,
                        },
                        new CommandSequenceStepRelative(100)
                        {
                            PanSpeed = 20,
                            TiltSpeed = 5.5,
                        },
                        //Drive 20° pan
                        //Drive 0.5s with tilt -5.5
                        new CommandSequenceStepRelative(100)
                        {
                            PanSpeed = 40,
                            TiltSpeed = -5.5,
                        },
                        new CommandSequenceStepRelative(100)
                        {
                            PanSpeed = 40,
                            TiltSpeed = -5.5,
                        },
                        new CommandSequenceStepRelative(100)
                        {
                            PanSpeed = 40,
                            TiltSpeed = -5.5,
                        },
                        new CommandSequenceStepRelative(100)
                        {
                            PanSpeed = 40,
                            TiltSpeed = -5.5,
                        },
                        new CommandSequenceStepRelative(100)
                        {
                            PanSpeed = 40,
                            TiltSpeed = -5.5,
                        },
                        //After these steps we must be very close to the position
                        new CommandSequenceStepAbsolute(1000)
                        {
                            Position = new PanTiltPosition(0, 0),
                            WaitPositionIsReached = false
                        },
                    }
                },
                new CommandSequence("Sequence AP RP 1")
                {
                    Steps = new CommandSequenceStep[]
                    {
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(-15, 0),
                            WaitPositionIsReached = true
                        },
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(-20, 0),
                            WaitPositionIsReached = true
                        },
                        //Drive 20x 5° pan
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(200)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        //After these steps we must be very close to the position
                        new CommandSequenceStepAbsolute(1000)
                        {
                            Position = new PanTiltPosition(0, 0),
                            WaitPositionIsReached = false
                        },
                    }
                },
                new CommandSequence("Sequence AP RP 2")
                {
                    Steps = new CommandSequenceStep[]
                    {
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(-5, 0),
                            WaitPositionIsReached = true
                        },
                        //Drive 20x 5° pan
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 5,
                            TiltSpeed = 0,
                        },
                        //After these steps we must be very close to the position
                        new CommandSequenceStepAbsolute(1000)
                        {
                            Position = new PanTiltPosition(0, 0),
                            WaitPositionIsReached = false
                        },
                    }
                },
                new CommandSequence("Sequence AT RT 1")
                {
                    Steps = new CommandSequenceStep[]
                    {
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(0, -5),
                            WaitPositionIsReached = true
                        },
                        //Drive 20x 5° pan
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        new CommandSequenceStepRelative(50)
                        {
                            PanSpeed = 0,
                            TiltSpeed = 5,
                        },
                        //After these steps we must be very close to the position
                        new CommandSequenceStepAbsolute(1000)
                        {
                            Position = new PanTiltPosition(0, 0),
                            WaitPositionIsReached = false
                        },
                    }
                },
                new CommandSequence("Sequence APT 1")
                {
                    Steps = new CommandSequenceStep[]
                    {
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(0, -5),
                        },
                        new CommandSequenceStepAbsolute(50)
                        {
                            Position = new PanTiltPosition(-10, -20),
                        },
                        new CommandSequenceStepAbsolute(50)
                        {
                            Position = new PanTiltPosition(20, 10),
                        },
                        new CommandSequenceStepAbsolute(0)
                        {
                            Position = new PanTiltPosition(0, 0),
                            WaitPositionIsReached = true
                        }
                    }
                }
            };

            this._commandSequences = this._commandSequences.OrderBy(o => o.Name).ToArray();

            var sequences = this._commandSequences.Select(o => o.Name).ToList();
            sequences.Insert(0, "All");

            this.comboBoxSequence.DataSource = sequences;
        }

        public void SetPanTiltControl(IPanTiltControl panTiltControl)
        {
            this._panTiltControl = panTiltControl;
            this._positionChecker = new PositionChecker(this._panTiltControl);
        }

        private async void buttonStartTest_Click(object sender, EventArgs e)
        {
            this._cancellationTokenSource?.Dispose();
            this._cancellationTokenSource = new CancellationTokenSource();

            this.buttonStartTest.Enabled = false;
            this.buttonAbort.Enabled = true;
            this._testResults.Clear();
            this._testBindingSource.ResetBindings(false);

            var selectedSequence = this.comboBoxSequence.Text;
            var sequences = this._commandSequences.AsQueryable();
            if (!selectedSequence.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                sequences = sequences.Where(o => o.Name.Equals(selectedSequence));
            }

            foreach (var sequence in sequences)
            {
                if (this._cancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }

                for (var repeat = 1; repeat <= 10; repeat++)
                {
                    await this.RunTestAsync(sequence, this._cancellationTokenSource.Token).ContinueWith(t =>
                    {
                        if (t.IsCanceled)
                        {
                            return;
                        }

                        this._testResults.Add(t.Result);
                    });

                    this._testBindingSource.ResetBindings(false);
                }
            }

            this.buttonAbort.Enabled = false;
            this.buttonStartTest.Enabled = true;
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            this._cancellationTokenSource?.Cancel();
        }

        private async Task<CommandSequenceResult> RunTestAsync(CommandSequence sequence, CancellationToken cancellationToken)
        {
            var result = new CommandSequenceResult
            {
                CreateDate = DateTime.Now,
                Name = sequence.Name,
            };

            foreach (var step in sequence.Steps)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return result;
                }

                switch (step.CommandType)
                {
                    case CommandSequenceType.Absolute:
                        var absoluteStep = step as CommandSequenceStepAbsolute;
                        this._panTiltControl.PanTiltAbsolute(absoluteStep.Position);
                        if (absoluteStep.WaitPositionIsReached)
                        {
                            await this._positionChecker.ComparePositionAsync(absoluteStep.Position, 0.1, cancellationToken: cancellationToken);
                        }
                        break;
                    case CommandSequenceType.Relative:
                        var relativeStep = step as CommandSequenceStepRelative;
                        this._panTiltControl.PanTiltRelative(relativeStep.PanSpeed, relativeStep.TiltSpeed);
                        break;
                    default:
                        break;
                }

                if (step.DelayAfterCommand > 0)
                {
                    await Task.Delay(step.DelayAfterCommand, cancellationToken);
                }
            }

            var lastStep = sequence.Steps.Last();
            var lastAbsoluteStep = lastStep as CommandSequenceStepAbsolute;
            var currentPosition = this._panTiltControl.GetPosition();

            if (PositionComparer.IsEqual(lastAbsoluteStep.Position, currentPosition, 0.2, 0.2))
            {
                result.Successful = true;
                return result;
            }

            result.Successful = false;
            result.Description = $"Last position not reached, Currrent position:{currentPosition} expected position:{lastAbsoluteStep.Position}";
            return result;
        }

        private void dataGridViewResult_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var item = this.dataGridViewResult.Rows[e.RowIndex].DataBoundItem as CommandSequenceResult;
            if (item.Successful)
            {
                this.dataGridViewResult.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.GreenYellow;
                return;
            }

            this.dataGridViewResult.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.OrangeRed;
        }
    }
}
