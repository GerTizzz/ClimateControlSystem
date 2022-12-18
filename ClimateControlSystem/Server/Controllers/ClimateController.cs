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
    public class ClimateController : ControllerBase
    {
        private readonly IClimateRepository _predictionRepository;

        public ClimateController(IClimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        [HttpGet("predictions/{recordsCount:int:range(1, 25)}")]
        public async Task<ActionResult<List<Prediction>>> GetPredictions(int recordsCount)
        {
            var records = await _predictionRepository.GetPredictionsWithAccuraciesAsync(recordsCount);

            return Ok(records);
        }

        [HttpGet("climatesdata/{recordsCount:int:range(1, 25)}")]
        public async Task<ActionResult<List<ClimateData>>> GetMonitorings(int recordsCount)
        {
            var records = await _predictionRepository.GetClimateData(recordsCount);

            return Ok(records);
        }
    }
}
