using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ClimateControlSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimateController : ControllerBase
    {
        private readonly IPredictionRepository _predictionRepository;
        private readonly IMapper _mapper;

        public ClimateController(IPredictionRepository predictionRepository, IMapper mapper)
        {
            _predictionRepository = predictionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{amountOfRecordsNeeeded:int:range(1, 25)}")]
        public async Task<ActionResult<MonitoringData>> GetClimateRecords(int amountOfRecordsNeeeded)
        {
            var records = await _predictionRepository.GetClimateRecordsAsync(amountOfRecordsNeeeded);

            return Ok(records);
        }
    }
}
