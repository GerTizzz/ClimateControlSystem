using ClimateControl.Server.Services.MediatR.Commands.ConfigManager;
using ClimateControl.Server.Services.MediatR.Queries.ConfigManager;
using ClimateControl.Shared.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClimateControl.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ConfigsDto>> GetConfig()
        {
            var config = await _mediator.Send(new GetConfigDtoQuery());

            return Ok(config);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateConfig(ConfigsDto config)
        {
            var result = await _mediator.Send(new UpdateConfigCommand(config));

            return Ok(result);
        }
    }
}
