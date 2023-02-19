using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Shared.SendToClient;
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
        private readonly IMicroclimateRepository _microclimateRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MicroclimateController(IMicroclimateRepository predictionRepository, IMapper mapper, IMediator mediator)
        {
            _microclimateRepository = predictionRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("monitoringscount")]
        public async Task<ActionResult<int>> GetMonitoringsCount()
        {
            var recordsCount = await _microclimateRepository.GetMonitoringsCountAsync();

            return Ok(recordsCount);
        }

        [HttpGet("monitorings/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<BaseMonitoringDTO>>> GetMonitorings(int start, int count)
        {
            var records = await _mediator.Send(new GetBaseMonitoringsQuery(new RequestLimits()
            {
                Start = start,
                Count = count
            }));

            return Ok(records);
        }

        [HttpGet("monitoringswithaccuracies/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringWithAccuraciesDTO>>> GetMonitoringsWithAccuracies(int start, int count)
        {
            var records = await _microclimateRepository.GetMonitoringsWithAccuraciesAsync(new RequestLimits()
            {
                Start = start,
                Count = count
            });

            var result = records.Select(rec => _mapper.Map<MonitoringWithAccuraciesDTO>(rec)).ToList();

            return Ok(result);
        }

        [HttpGet("microclimatescount")]
        public async Task<ActionResult<int>> GetMicroclimatesCount()
        {
            var recordsCount = await _microclimateRepository.GetMicroclimatesCountAsync();

            return Ok(recordsCount);
        }

        [HttpGet("microclimates/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MicroclimateDTO>>> GetMicroclimates(int start, int count)
        {
            var records = await _microclimateRepository.GetMicroclimatesAsync(new RequestLimits()
            {
                Start = start,
                Count = count
            });

            return Ok(records.ToList());
        }

        [HttpGet("monitoringevents/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringEventsDTO>>> GetMonitoringEvents(int start, int count)
        {
            var records = await _microclimateRepository.GetMonitoringEventsAsync(new RequestLimits()
            {
                Start = start,
                Count = count
            });

            return Ok(records);
        }
    }
}
