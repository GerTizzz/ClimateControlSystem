using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Server.Services
{
    public sealed class ConfigManager : IConfigManager
    {
        private readonly IConfigRepository _configRepository;

        private Config _config;

        public Config Config => _config;

        public float UpperTemperatureWarningLimit => _config.UpperTemperatureWarningLimit;
        public float LowerTemperatureWarningLimit => _config.LowerTemperatureWarningLimit;

        public float UpperHumidityWarningLimit => _config.UpperHumidityWarningLimit;
        public float LowerHumidityWarningLimit => _config.LowerHumidityWarningLimit;

        public ConfigManager(IConfigRepository configRepository)
        {
            _configRepository = configRepository;

            var conf = _configRepository.GetConfig();

            Task.WaitAll(conf);

            _config = conf.Result;
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
