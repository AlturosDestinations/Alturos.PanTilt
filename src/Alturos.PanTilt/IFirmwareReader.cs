using System.Threading.Tasks;

namespace Alturos.PanTilt
{
    public interface IFirmwareReader
    {
        Task<string> GetFirmwareAsync();
    }
}
