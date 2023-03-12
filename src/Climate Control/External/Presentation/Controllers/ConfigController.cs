using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ConfigsDto>> GetConfig(CancellationToken cancellationToken)
        {
            var config = await Sender.Send(new GetConfigDtoQuery(), cancellationToken);

            return Ok(config);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateConfig(ConfigsDto config, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateConfigCommand(config), cancellationToken);

            return Ok(result);
        }
    }
}
