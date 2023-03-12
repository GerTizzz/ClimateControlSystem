using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        [HttpGet("monitoringscount")]
        public async Task<ActionResult<long>> GetMonitoringsCount()
        {
            var recordsCount = await Sender.Send(new GetMonitoringsCountQuery());

            return Ok(recordsCount);
        }

        [HttpGet("monitoringseventscount")]
        public async Task<ActionResult<long>> GetMicroclimatesEventsCount()
        {
            var recordsCount = await _mediator.Send(new GetWarningsCountQuery());

            return Ok(recordsCount);
        }

        [HttpGet("monitorings/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<BaseMonitoringDto>>> GetMonitorings(int start, int count)
        {
            var records = await _mediator.Send(new GetBaseMonitoringsQuery(new DbRequest(start, count)));

            return Ok(records);
        }

        [HttpGet("monitoringswithaccuracies/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringWithAccuracyDto>>> GetMonitoringsWithAccuracies(int start, int count)
        {
            var records = await _mediator.Send(new GetMonitoringsWithAccuracyQuery(new DbRequest(start, count)));

            return Ok(records);
        }

        [HttpGet("monitoringsforecastings/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<ForecastingDto>>> GetForecastings(int start, int count)
        {
            var records = await _mediator.Send(new GetForecastsDtosQuery(new DbRequest(start, count)));

            return Ok(records);
        }

        [HttpGet("monitoringsevents/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringsEventsDto>>> GetMonitoringEvents(int start, int count)
        {
            var records = await _mediator.Send(new GetMonitoringEventsQuery(new DbRequest(start, count)));

            return Ok(records);
        }
    }
}
