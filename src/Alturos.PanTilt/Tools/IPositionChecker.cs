using System.Threading;
using System.Threading.Tasks;

namespace Alturos.PanTilt.Tools
{
    public interface IPositionChecker
    {
        /// <summary>
        /// Check PanTilt Position
        /// </summary>
        /// <param name="position">expect pan tilt position</param>
        /// <param name="tolerance">tolerance to expect position</param>
        /// <param name="retry">retry count</param>
        /// <param name="timeout">Waiting time until the next attempt (milliseconds)</param>
        /// <returns></returns>
        bool ComparePosition(PanTiltPosition position, double tolerance = 0.5, int retry = 5, int timeout = 500);

        /// <summary>
        /// Check PanTilt Position
        /// </summary>
        /// <param name="position">expect pan tilt position</param>
        /// <param name="tolerance">tolerance to expect position</param>
        /// <param name="retry">retry count</param>
        /// <param name="timeout">Waiting time until the next attempt (milliseconds)</param>
        /// <returns></returns>
        Task<bool> ComparePositionAsync(PanTiltPosition position, double tolerance = 0.5, int retry = 5, int timeout = 500, CancellationToken cancellationToken = default(CancellationToken));
    }
}
