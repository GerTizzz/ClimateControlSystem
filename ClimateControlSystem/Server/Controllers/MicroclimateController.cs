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
        private readonly IMicroclimateRepository _predictionRepository;

        public MicroclimateController(IMicroclimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        [HttpGet("monitorings/{recordsCount:int:range(1, 25)}")]
        public async Task<ActionResult<List<Monitoring>>> GetMonitorings(int recordsCount)
        {
            var records = await _predictionRepository.GetMonitorings(recordsCount);

            return Ok(records);
        }

        [HttpGet("microclimates/{recordsCount:int:range(1, 25)}")]
        public async Task<ActionResult<List<MicroclimateData>>> GetMicroclimateData(int recordsCount)
        {
            var records = await _predictionRepository.GetMicroclimateDataAsync(recordsCount);

            return Ok(records);
        }
    }
}
