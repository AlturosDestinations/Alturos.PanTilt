using System;

namespace Alturos.PanTilt
{
    public interface IPanTiltControl : IDisposable
    {
        /// <summary>
        /// Start the communication with the pt head
        /// </summary>
        /// <returns></returns>
        bool Start();

        /// <summary>
        /// Stop the communication with the pt head
        /// </summary>
        /// <returns></returns>
        bool Stop();

        /// <summary>
        /// Event triggers new pt head position received
        /// </summary>
        event Action<PanTiltPosition> PositionChanged;

        /// <summary>
        /// Event triggers pt head limits changed
        /// </summary>
        event Action LimitChanged;

        /// <summary>
        /// Event triggers pt head detect a failure on limit 
        /// </summary>
        event Action LimitOverrun;

        #region Absolute

        /// <summary>
        /// Pan move to an absolute position
        /// </summary>
        /// <param name="pan"></param>
        /// <returns></returns>
        bool PanAbsolute(double pan);

        /// <summary>
        /// Tilt move to an absolute position
        /// </summary>
        /// <param name="tilt"></param>
        /// <returns></returns>
        bool TiltAbsolute(double tilt);

        /// <summary>
        /// Pan Tilt move to an absolute position
        /// </summary>
        /// <param name="pan"></param>
        /// <param name="tilt"></param>
        /// <returns></returns>
        bool PanTiltAbsolute(double pan, double tilt);

        #endregion

        #region Relative

        /// <summary>
        /// Pan move of the pt head with a relative speed
        /// </summary>
        /// <param name="panSpeed">degree per second</param>
        bool PanRelative(double panSpeed);

        /// <summary>
        /// Tilt move of the pt head with a relative speed
        /// </summary>
        /// <param name="tiltSpeed">degree per second</param>
        bool TiltRelative(double tiltSpeed);

        /// <summary>
        /// Pan Tilt move of the pt head with a relative speed
        /// </summary>
        /// <param name="panSpeed">degree per second</param>
        /// <param name="tiltSpeed">degree per second</param>
        bool PanTiltRelative(double panSpeed, double tiltSpeed);

        /// <summary>
        /// Stop Relative movement
        /// </summary>
        /// <returns></returns>
        bool StopMoving();

        #endregion

        /// <summary>
        /// Get the current pt position
        /// </summary>
        /// <returns></returns>
        PanTiltPosition GetPosition();

        /// <summary>
        /// Get the limits of the pt head
        /// </summary>
        /// <returns></returns>
        PanTiltLimit GetLimits();

        /// <summary>
        /// Set the limits of the pt head
        /// </summary>
        /// <returns></returns>
        bool SetLimits(PanTiltLimit panTiltLimit);

        /// <summary>
        /// Reinitialize the pt head
        /// </summary>
        /// <returns></returns>
        bool ReinitializePosition();
    }
}
