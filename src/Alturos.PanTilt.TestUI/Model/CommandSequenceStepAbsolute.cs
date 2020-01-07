namespace Alturos.PanTilt.TestUI.Model
{
    public class CommandSequenceStepAbsolute : CommandSequenceStep
    {
        public PanTiltPosition Position { get; set; }
        public bool WaitPositionIsReached { get; set; }

        public CommandSequenceStepAbsolute(int delayAfterCommand) : base(CommandSequenceType.Absolute, delayAfterCommand)
        {
        }
    }
}
