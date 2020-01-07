using System;

namespace Alturos.PanTilt.TestUI.Model
{
    public class CommandSequenceResult
    {
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public int Repeat { get; set; }
        public bool Successful { get; set; }
        public string Description { get; set; }
    }
}
