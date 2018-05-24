using System;

namespace Alturos.PanTilt
{
    public interface IPanTiltControl : IDisposable
    {
        bool Start();
        bool Stop();

        event Action<PanTiltPosition> PositionChanged;
        event Action LimitOverrun;
        event Action LimitChanged;

        void PanAbsolute(double pan);
        void TiltAbsolute(double tilt);
        void PanTiltAbsolute(double pan, double tilt);
        /// <summary>
        /// PanRelative
        /// </summary>
        /// <param name="panSpeed">degree per second</param>
        void PanRelative(double panSpeed);
        /// <summary>
        /// TiltRelative
        /// </summary>
        /// <param name="tiltSpeed">degree per second</param>
        void TiltRelative(double tiltSpeed);
        /// <summary>
        /// PanTiltRelative
        /// </summary>
        /// <param name="panSpeed">degree per second</param>
        /// <param name="tiltSpeed">degree per second</param>
        void PanTiltRelative(double panSpeed, double tiltSpeed);
        PanTiltPosition GetPosition();
        PanTiltLimit GetLimits();
        bool StopMoving();

        /// <summary>
        /// Check PanTilt Position
        /// </summary>
        /// <param name="position">expect pan tilt position</param>
        /// <param name="tolerance">tolerance to position</param>
        /// <param name="retry">retry count</param>
        /// <param name="timeout">milliseconds</param>
        /// <returns></returns>
        bool ComparePosition(PanTiltPosition position, double tolerance = 0.5, int retry = 5, int timeout = 500);

        void ReinitializePosition();
    }
}
