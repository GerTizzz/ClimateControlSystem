using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources;
using ClimateControlSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClimateControlSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ClimateController : ControllerBase
    {
        private readonly IMonitoringDataRepository _predictionRepository;

        public ClimateController(IMonitoringDataRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        [HttpGet]
        [Route("{amountOfRecordsNeeeded:int:range(1, 25)}")]
        public async Task<ActionResult<List<MonitoringData>>> GetClimateRecords(int amountOfRecordsNeeeded)
        {
            var records = await _predictionRepository.GetClimateRecordsAsync(amountOfRecordsNeeeded);

            return Ok(records);
        }
    }
}
