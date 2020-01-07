namespace Alturos.PanTilt.TestUI.Model
{
    public class CommandSequence
    {
        public string Name { get; set; }
        public CommandSequenceStep[] Steps { get; set; }

        public CommandSequence(string name)
        {
            this.Name = name;
        }
    }
}
