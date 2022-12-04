using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Server.Services
{
    public sealed class ConfigManager : IConfigManager
    {
        private readonly IConfigRepository _configRepository;

        private Config _config;

        public float TemperatureLimit => _config.TemperatureLimit;
        public float HumidityLimit => _config.HumidityLimit;

        public ConfigManager(IConfigRepository configRepository)
        {
            _configRepository = configRepository;

            _config = _configRepository.GetConfig().Result;
        }

        public Task<Config> GetConfigAsync()
        {
            return Task.FromResult(_config);
        }

        public async Task<bool> UpdateConfig(Config config)
        {
            bool isUpdated = await _configRepository.UpdateConfig(config);

            if (isUpdated)
            {
                _config = config;
            }

            return isUpdated;
        }
    }
}
