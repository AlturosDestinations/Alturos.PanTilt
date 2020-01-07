namespace Alturos.PanTilt.TestUI.Model
{
    public class CommandSequenceStepRelative : CommandSequenceStep
    {
        public double PanSpeed { get; set; }
        public double TiltSpeed { get; set; }

        public CommandSequenceStepRelative(int delayAfterCommand) : base(CommandSequenceType.Relative, delayAfterCommand)
        {
        }
    }
}
