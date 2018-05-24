using System;
using System.Threading.Tasks;

namespace Alturos.PanTilt.TestUI.Contract
{
    public interface IZoomProvider : IDisposable
    {
        event Action<double> ZoomChanged;
        double GetZoom();
        Task<bool> SetZoomAsync(double zoom);
    }
}
