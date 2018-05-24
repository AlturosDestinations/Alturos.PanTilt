using System;
using System.Threading.Tasks;

namespace Alturos.PanTilt.TestUI.Contract
{
    public class MockZoomProvider : IZoomProvider
    {
        private double _currentZoom = 0;
        public event Action<double> ZoomChanged;

        public MockZoomProvider()
        {
            this._currentZoom = 0;
        }

        public void Dispose()
        {
        }

        public double GetZoom()
        {
            return this._currentZoom = 0;
        }

        public Task<bool> SetZoomAsync(double zoom)
        {
            this._currentZoom = zoom;
            this.ZoomChanged?.Invoke(zoom);
            return Task.FromResult(true);
        }
    }
}