using log4net;
using System;
using System.Threading;

namespace Alturos.PanTilt
{
    public class PositionChecker : IPositionChecker
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PositionChecker));
        private readonly IPanTiltControl _panTiltControl;
        private readonly bool _debug = false;

        public PositionChecker(IPanTiltControl panTiltControl, bool debug = false)
        {
            this._panTiltControl = panTiltControl;
            this._debug = false;
        }

        public bool ComparePosition(PanTiltPosition position, double tolerance = 0.5, int retry = 5, int timeout = 500)
        {
            if (this._debug)
            {
                Log.Debug($"{nameof(ComparePosition)} - Pan:{position.Pan} Tilt:{position.Tilt} Tolerance: {tolerance}");
            }

            while (retry > 0)
            {
                var ptHeadPosition = this._panTiltControl.GetPosition();

                if (ptHeadPosition == null)
                {
                    Thread.Sleep(timeout);
                    retry--;
                    continue;
                }

                var panDifference = Math.Abs(position.Pan - ptHeadPosition.Pan);
                var tiltDifference = Math.Abs(position.Tilt - ptHeadPosition.Tilt);

                if (panDifference <= tolerance && tiltDifference <= tolerance)
                {
                    return true;
                }

                if (this._debug)
                {
                    Log.Debug($"{nameof(ComparePosition)} - Difference, Pan:{panDifference} Tilt:{tiltDifference} Retry:{retry}");
                }

                Thread.Sleep(timeout);
                retry--;
            }

            return false;
        }
    }
}
