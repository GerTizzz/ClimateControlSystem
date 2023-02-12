using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Shared.SendToClient;
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

        public MicroclimateController(IMicroclimateRepository predictionRepository, IMapper mapper)
        {
            _microclimateRepository = predictionRepository;
            _mapper = mapper;
        }

        [HttpGet("monitoringscount")]
        public async Task<ActionResult<int>> GetMonitoringsCount()
        {
            var recordsCount = await _microclimateRepository.GetMonitoringsCountAsync();

            return Ok(recordsCount);
        }

        [HttpGet("monitorings/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<BaseMonitoringResponse>>> GetMonitorings(int start, int count)
        {
            var records = await _microclimateRepository.GetMonitoringsAsync(start, count);

            var result = records.Select(rec => _mapper.Map<BaseMonitoringResponse>(rec)).ToList();

            return Ok(result);
        }

        [HttpGet("monitoringswithaccuracies/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringWithAccuraciesResponse>>> GetMonitoringsWithAccuracies(int start, int count)
        {
            var records = await _microclimateRepository.GetMonitoringsWithAccuraciesAsync(start, count);

            var result = records.Select(rec => _mapper.Map<MonitoringWithAccuraciesResponse>(rec)).ToList();

            return Ok(result);
        }

        [HttpGet("microclimatescount")]
        public async Task<ActionResult<int>> GetMicroclimatesCount()
        {
            var recordsCount = await _microclimateRepository.GetMicroclimatesCountAsync();

            return Ok(recordsCount);
        }

        [HttpGet("microclimates/{offsetFromTheEnd:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MicroclimateResponse>>> GetMicroclimates(int offsetFromTheEnd, int count)
        {
            var records = await _microclimateRepository.GetMicroclimatesAsync(offsetFromTheEnd, count);

            return Ok(records.ToList());
        }

        [HttpGet("monitoringevents/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringEventsResponse>>> GetMonitoringEvents(int start, int count)
        {
            var records = await _microclimateRepository.GetMonitoringEventsAsync(start, count);

            return Ok(records);
        }
    }
}
