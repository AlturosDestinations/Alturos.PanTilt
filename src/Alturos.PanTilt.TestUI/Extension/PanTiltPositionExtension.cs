namespace Alturos.PanTilt.TestUI.Extension
{
    public static class PanTiltPositionExtension
    {
        public static PanTiltPosition AddRelativePosition(this PanTiltPosition currentPanTiltPosition, PanTiltPosition panTiltPosition, int seconds)
        {
            var pan = currentPanTiltPosition.Pan + panTiltPosition.Pan * seconds;
            var tilt = currentPanTiltPosition.Tilt + panTiltPosition.Tilt * seconds;

            return new PanTiltPosition(pan, tilt);
        }
    }
}
