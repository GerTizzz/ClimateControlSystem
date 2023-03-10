using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using WebApi.Resources.Infrastructure;
using WebApi.Services.MediatR.Queries.MonitoringsRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MonitoringController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("monitoringscount")]
        public async Task<ActionResult<long>> GetMonitoringsCount()
        {
            var recordsCount = await _mediator.Send(new GetMonitoringsCountQuery());

            return Ok(recordsCount);
        }

        [HttpGet("monitoringseventscount")]
        public async Task<ActionResult<long>> GetMicroclimatesEventsCount()
        {
            var recordsCount = await _mediator.Send(new GetMicroclimatesEventsCountQuery());

            return Ok(recordsCount);
        }

        [HttpGet("monitorings/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<BaseMonitoringDto>>> GetMonitorings(int start, int count)
        {
            var records = await _mediator.Send(new GetBaseMonitoringsQuery(new RequestLimits(start, count)));

            return Ok(records);
        }

        [HttpGet("monitoringswithaccuracies/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringWithAccuracyDto>>> GetMonitoringsWithAccuracies(int start, int count)
        {
            var records = await _mediator.Send(new GetMonitoringsWithAccuracyQuery(new RequestLimits(start, count)));

            return Ok(records);
        }

        [HttpGet("monitoringsforecastings/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<ForecastingDto>>> GetForecastings(int start, int count)
        {
            var records = await _mediator.Send(new GetMicroclimatesQuery(new RequestLimits(start, count)));

            return Ok(records);
        }

        [HttpGet("monitoringsevents/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringsEventsDto>>> GetMonitoringEvents(int start, int count)
        {
            var records = await _mediator.Send(new GetMonitoringEventsQuery(new RequestLimits(start, count)));

            return Ok(records);
        }
    }
}
