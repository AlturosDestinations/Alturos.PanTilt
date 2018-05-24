using System;
using System.Collections.Generic;

namespace Alturos.PanTilt.Diagnostic
{
    public class CircleLogic
    {
        public List<PanTiltPosition> CalculatePtPositions(float circleRadius, int detailLevel)
        {
            var ptPositions = new List<PanTiltPosition>();

            var degree = this.GetDegreeOutOfLevel(detailLevel);
            if (degree < 5)
            {
                return ptPositions;
            }

            var z = degree / 180.0 * Math.PI;

            ptPositions.Add(new PanTiltPosition(Convert.ToSingle(circleRadius), Convert.ToSingle(0)));

            var j = 1;
            for (var i = z; i <= Math.PI * 2.0; i += z)
            {
                var ptX = circleRadius * Math.Cos(i);
                var ptY = circleRadius * Math.Sin(i);

                ptPositions.Add(new PanTiltPosition(Math.Round(ptX, 2), Math.Round(ptY, 2)));
                j += 1;

                //Fallback
                if (j > 200)
                {
                    break;
                }
            }

            return ptPositions;
        }

        private int GetDegreeOutOfLevel(int value)
        {
            //Returns result of inverse function to calculate degree out of selected level
            //e.g. Level 1 = Degree 20; Level 15 = Degree 5
            return (int)(-0.9333333 * value + 19.666667);
        }
    }
}
