using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Shared;
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

        public MicroclimateController(IMicroclimateRepository predictionRepository)
        {
            _microclimateRepository = predictionRepository;
        }

        [HttpGet("monitorings/{recordsCount:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringResponse>>> GetMonitorings(int recordsCount)
        {
            var records = await _microclimateRepository.GetMonitorings(recordsCount);

            return Ok(records);
        }

        [HttpGet("microclimates/{recordsCount:int:range(1, 25)}")]
        public async Task<ActionResult<List<MicroclimateResponse>>> GetMicroclimateData(int recordsCount)
        {
            var records = await _microclimateRepository.GetMicroclimateDataAsync(recordsCount);

            return Ok(records);
        }

        [HttpGet("temperatureevents/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MicroclimateResponse>>> GetTemperatureEvents(int start, int count)
        {
            var records = await _microclimateRepository.GetTemperatureEventsAsync(start, count);

            return Ok(records);
        }

        [HttpGet("humidityevents/{start:int}/{count:int:range(1, 25)}")]
        public async Task<ActionResult<List<MicroclimateResponse>>> GetHumidityEvents(int start, int count)
        {
            var records = await _microclimateRepository.GetHumidityEventsAsync(start, count);

            return Ok(records);
        }
    }
}
