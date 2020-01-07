using System;

namespace Alturos.PanTilt.Tools
{
    public static class PositionComparer
    {
        public static bool IsEqual(PanTiltPosition position1, PanTiltPosition position2, double panTolerance = 0.5, double tiltTolerance = 0.5)
        {
            var panDifference = Math.Abs(position1.Pan - position2.Pan);
            var tiltDifference = Math.Abs(position1.Tilt - position2.Tilt);

            if (panDifference <= panTolerance && tiltDifference <= tiltTolerance)
            {
                return true;
            }

            return false;
        }
    }
}
