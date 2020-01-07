namespace Alturos.PanTilt.TestUI.Model
{
    public abstract class CommandSequenceStep
    {
        public CommandSequenceType CommandType { get; private set; }
        public int DelayAfterCommand { get; private set; }

        protected CommandSequenceStep(CommandSequenceType commandType, int delayAfterCommand)
        {
            this.CommandType = commandType;
            this.DelayAfterCommand = delayAfterCommand;
        }
    }
}
