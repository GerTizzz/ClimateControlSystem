using AutoMapper;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClimateControlSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigManager _configManager;
        private readonly IMapper _mapper;

        public ConfigController(IConfigManager configManager, IMapper mapper)
        {
            _configManager = configManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ConfigsDTO>> GetConfig()
        {
            var response = _mapper.Map<ConfigsDTO>(_configManager.Config);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateConfig(ConfigsDTO newConfig)
        {
            var config = _mapper.Map<Config>(newConfig);

            var result = await _configManager.UpdateConfig(config);

            return Ok(result);
        }
    }
}
