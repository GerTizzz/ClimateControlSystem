using Application.MediatR.WarningsRepository;
using Application.Primitives;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public sealed class WarningsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WarningsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("count")]
        public async Task<ActionResult<long>> GetWarningsCount()
        {
            var recordsCount = await _mediator.Send(new GetWarningsCountQuery());

            return Ok(recordsCount);
        }

        [HttpGet("interval/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<WarningDto>>> GetWarnings(int start, int count)
        {
            var records = await _mediator.Send(new GetWarningsQuery(new DbRequest(start, count)));

            return Ok(records);
        }
    }
}
