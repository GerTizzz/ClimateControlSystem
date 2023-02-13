﻿using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Domain.Singletons;
using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Services
{
    public sealed class ConfigManager : IConfigManager
    {
        private readonly IConfigRepository _configRepository;
        private readonly IConfigSingleton _configSingleton;

        private Config _config => _configSingleton.Config;

        public Config Config => _config;

        public float UpperTemperatureWarningLimit => _config.UpperTemperatureWarningLimit;
        public float LowerTemperatureWarningLimit => _config.LowerTemperatureWarningLimit;

        public float UpperHumidityWarningLimit => _config.UpperHumidityWarningLimit;
        public float LowerHumidityWarningLimit => _config.LowerHumidityWarningLimit;

        public int PredictionTimeIntervalSeconds => _config.PredictionTimeIntervalSeconds;

        public ConfigManager(IConfigRepository configRepository, IConfigSingleton configSingleton)
        {
            _configRepository = configRepository;
            _configSingleton = configSingleton;

            var config = _configRepository.GetConfigAsync();

            Task.WaitAll(config);

            _configSingleton.TrySetInitialConfig(config.Result);
        }

        public async Task<bool> UpdateConfig(Config config)
        {
            bool isUpdated = await _configRepository.UpdateConfigAsync(config);

            if (isUpdated)
            {
                await _configSingleton.UpdateConfig(config);
            }

            return isUpdated;
        }
    }
}
