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
        /// Reinitialize the pt head
        /// </summary>
        /// <returns></returns>
        bool ReinitializePtHead();

        #region Position

        /// <summary>
        /// Event triggers new pt head position received
        /// </summary>
        event Action<PanTiltPosition> PositionChanged;

        /// <summary>
        /// Get the current pt position
        /// </summary>
        /// <returns></returns>
        PanTiltPosition GetPosition();

        #endregion

        #region Absolute

        /// <summary>
        /// Move Pan to an absolute position
        /// </summary>
        /// <param name="degree">degree</param>
        /// <returns></returns>
        bool PanAbsolute(double degree);

        /// <summary>
        /// Move Tilt to an absolute position
        /// </summary>
        /// <param name="degree">degree</param>
        /// <returns></returns>
        bool TiltAbsolute(double degree);

        /// <summary>
        /// Move Pan and Tilt to an absolute position
        /// </summary>
        /// <param name="panDegree">degree</param>
        /// <param name="tiltDegree">degree</param>
        /// <returns></returns>
        bool PanTiltAbsolute(double panDegree, double tiltDegree);

        #endregion

        #region Relative

        /// <summary>
        /// Pan move of the pt head with a relative speed
        /// </summary>
        /// <param name="degreePerSecond">degree/s</param>
        bool PanRelative(double degreePerSecond);

        /// <summary>
        /// Tilt move of the pt head with a relative speed
        /// </summary>
        /// <param name="degreePerSecond">degree/s</param>
        bool TiltRelative(double degreePerSecond);

        /// <summary>
        /// Pan Tilt move of the pt head with a relative speed
        /// </summary>
        /// <param name="panDegreePerSecond">degree/s</param>
        /// <param name="tiltDegreePerSecond">degree/s</param>
        bool PanTiltRelative(double panDegreePerSecond, double tiltDegreePerSecond);

        /// <summary>
        /// Stop Relative movement
        /// </summary>
        /// <returns></returns>
        bool StopMoving();

        #endregion

        #region Limits

        /// <summary>
        /// Event triggers pt head limits changed
        /// </summary>
        event Action LimitChanged;

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

        #endregion
    }
}
