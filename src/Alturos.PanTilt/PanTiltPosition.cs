namespace Alturos.PanTilt
{
    public class PanTiltPosition
    {
        public double Pan { get; set; }
        public double Tilt { get; set; }

        public PanTiltPosition() { }

        public PanTiltPosition(double pan, double tilt)
        {
            this.Pan = pan;
            this.Tilt = tilt;
        }

        protected bool Equals(PanTiltPosition other)
        {
            return this.Pan == other.Pan && this.Tilt == other.Tilt;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PanTiltPosition);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ this.Pan.GetHashCode();
                result = (result * 397) ^ this.Tilt.GetHashCode();
                return result;
            }
        }

        public override string ToString()
        {
            return $"{this.Pan}/{this.Tilt}";
        }

        public PanTiltPosition AddRelativePosition(PanTiltPosition panTiltPosition, int seconds)
        {
            var pan = this.Pan + panTiltPosition.Pan * seconds;
            var tilt = this.Tilt + panTiltPosition.Tilt * seconds;

            return new PanTiltPosition(pan,tilt);
        }
    }
}
