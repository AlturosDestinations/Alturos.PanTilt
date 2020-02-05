using Alturos.PanTilt.Communication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace Alturos.PanTilt.Website.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PanTiltUnitController : ControllerBase
    {
        private readonly ILogger<PanTiltUnitController> _logger;

        public PanTiltUnitController(ILogger<PanTiltUnitController> logger)
        {
            this._logger = logger;
        }

        [HttpPost]
        [Route("MoveToPosition")]
        public ActionResult MoveToPosition([FromQuery] string ipAddress, [FromBody] PanTiltPosition position)
        {
            this._logger.LogDebug($"MoveToPosition {ipAddress} {position}");
            using (ICommunication communication = new UdpNetworkCommunication(IPAddress.Parse(ipAddress), 5555))
            using (IPanTiltControl panTiltControl = new AlturosPanTiltControl(communication))
            {
                if (panTiltControl.PanTiltAbsolute(position))
                {
                    return StatusCode(StatusCodes.Status200OK);
                }

                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet]
        [Route("GetCurrentPosition")]
        public async Task<ActionResult<PanTiltPosition>> GetCurrentPositionAsync([FromQuery] string ipAddress)
        {
            this._logger.LogDebug($"GetCurrentPosition {ipAddress}");
            using (ICommunication communication = new UdpNetworkCommunication(IPAddress.Parse(ipAddress), 5555))
            using (IPanTiltControl panTiltControl = new AlturosPanTiltControl(communication))
            {
                if (!panTiltControl.Start())
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                await Task.Delay(200);

                var position = panTiltControl.GetPosition();

                if (!panTiltControl.Stop())
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return StatusCode(StatusCodes.Status200OK, position);
            }
        }
    }
}
