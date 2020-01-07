namespace Alturos.PanTilt
{
    public class PanTiltPosition
    {
        /// <summary>
        /// Pan (degree)
        /// </summary>
        public double Pan { get; set; }
        /// <summary>
        /// Tilt (degree)
        /// </summary>
        public double Tilt { get; set; }

        public PanTiltPosition() { }

        /// <summary>
        /// PanTiltPosition
        /// </summary>
        /// <param name="pan">degree</param>
        /// <param name="tilt">degree</param>
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
    }
}
