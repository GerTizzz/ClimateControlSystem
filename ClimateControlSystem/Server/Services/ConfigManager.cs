﻿using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Domain.Singletons;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Commands.ConfigRepository;
using MediatR;

namespace ClimateControlSystem.Server.Services
{
    public sealed class ConfigManager : IConfigManager
    {
        private readonly IConfigSingleton _configSingleton;
        private readonly IMediator _mediator;

        public Config Config => _configSingleton.Config;

        public float UpperTemperatureWarningLimit => Config.UpperTemperatureWarningLimit;
        public float LowerTemperatureWarningLimit => Config.LowerTemperatureWarningLimit;

        public float UpperHumidityWarningLimit => Config.UpperHumidityWarningLimit;
        public float LowerHumidityWarningLimit => Config.LowerHumidityWarningLimit;

        public int PredictionTimeIntervalSeconds => Config.PredictionTimeIntervalSeconds;

        public ConfigManager(IConfigSingleton configSingleton, IMediator mediator)
        {
            _configSingleton = configSingleton;
            _mediator = mediator;
        }

        public async Task<bool> UpdateConfig(Config config)
        {
            bool updateResult = await _mediator.Send(new UpdateConfigsEntityCommand(config));

            if (updateResult)
            {
                _configSingleton.UpdateConfig(config);
            }

            return updateResult;
        }
    }
}
