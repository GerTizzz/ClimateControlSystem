using Application.MediatR.ForecastsRepository;
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
    public class ForecastsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ForecastsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("count")]
        public async Task<ActionResult<long>> GetForecastsCount()
        {
            var recordsCount = await _mediator.Send(new GetForecastsCountQuery());

            return Ok(recordsCount);
        }

        [HttpGet("interval/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<ForecastDto>>> GetForecasts(int start, int count)
        {
            var records = await _mediator.Send(new GetForecastsQuery(new DbRequest(start, count)));

            return Ok(records);
        }
    }
}
