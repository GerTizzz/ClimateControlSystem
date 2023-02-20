using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository;
using ClimateControlSystem.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClimateControlSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MicroclimateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MicroclimateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("monitoringscount")]
        public async Task<ActionResult<int>> GetMonitoringsCount()
        {
            var recordsCount = await _mediator.Send(new GetMonitoringsCountQuery());

            return Ok(recordsCount);
        }

        [HttpGet("monitorings/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<BaseMonitoringDTO>>> GetMonitorings(int start, int count)
        {
            var records = await _mediator.Send(new GetBaseMonitoringsQuery(new RequestLimits(start, count)));

            return Ok(records);
        }

        [HttpGet("monitoringswithaccuracies/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringWithAccuracyDTO>>> GetMonitoringsWithAccuracies(int start, int count)
        {
            var records = await _mediator.Send(new GetMonitoringsWithAccuracyQuery(new RequestLimits(start, count)));

            return Ok(records);
        }

        [HttpGet("microclimatescount")]
        public async Task<ActionResult<int>> GetMicroclimatesCount()
        {
            var recordsCount = await _mediator.Send(new GetMicroclimatesCountQuery());

            return Ok(recordsCount);
        }

        [HttpGet("microclimates/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MicroclimateDTO>>> GetMicroclimates(int start, int count)
        {
            var records = await _mediator.Send(new GetMicroclimatesQuery(new RequestLimits(start, count)));

            return Ok(records);
        }

        [HttpGet("monitoringevents/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringEventsDTO>>> GetMonitoringEvents(int start, int count)
        {
            var records = await _mediator.Send(new GetMonitoringEventsQuery(new RequestLimits(start, count)));

            return Ok(records);
        }
    }
}
