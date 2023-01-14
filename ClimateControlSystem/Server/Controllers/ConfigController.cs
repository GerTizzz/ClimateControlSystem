using ClimateControlSystem.Server.Domain.Services;
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

        public ConfigController(IConfigManager configManager)
        {
            _configManager = configManager;
        }

        [HttpGet]
        public async Task<ActionResult<Config>> GetConfig()
        {
            return Ok(_configManager.Config);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateConfig(Config newConfig)
        {
            var result = await _configManager.UpdateConfig(newConfig);

            return Ok(result);
        }
    }
}
