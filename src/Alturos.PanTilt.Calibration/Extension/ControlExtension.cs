using System;
using System.Windows.Forms;

namespace Alturos.PanTilt.Calibration.Extension
{
    public static class ControlExtension
    {
        public static void Invoke<T>(this T c, Action<T> action) where T : Control
        {
            if (c.InvokeRequired)
            {
                c.BeginInvoke(new Action(() => action(c)));
            }
            else
            {
                action(c);
            }
        }
    }
}
