﻿using Application.MediatR.ConfigsManager;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ConfigsDto>> GetConfig(CancellationToken cancellationToken)
        {
            var config = await _mediator.Send(new GetConfigDtoQuery(), cancellationToken);

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