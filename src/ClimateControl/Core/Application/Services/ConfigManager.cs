using Application.MediatR.ConfigRepository;
using Domain.Services;
using Domain.Singletons;
using MediatR;

namespace Application.Services
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
            bool updateResult = await _mediator.Send(new UpdateConfigCommand(config));

            if (updateResult)
            {
                _configSingleton.UpdateConfig(config);
            }

            return updateResult;
        }
    }
}
