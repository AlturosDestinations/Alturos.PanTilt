using System;

namespace Alturos.PanTilt.TestUI.Model
{
    public class FastMovementInfo
    {
        public DateTime Timestamp { get; }
        public int Request { get; }
        public string Type { get; }
        public double Pan { get; }
        public double Tilt { get; }

        public FastMovementInfo(DateTime timestamp, int request, string type, double pan, double tilt)
        {
            this.Timestamp = timestamp;
            this.Request = request;
            this.Type = type;
            this.Pan = pan;
            this.Tilt = tilt;
        }
    }
}
