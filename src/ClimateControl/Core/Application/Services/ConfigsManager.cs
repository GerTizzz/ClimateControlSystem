using Application.MediatR.ConfigsRepository;
using Domain.Services;
using Domain.Singletons;
using MediatR;

namespace Application.Services;

public sealed class ConfigsManager : IConfigsManager
{
    private readonly IConfigSingleton _configSingleton;
    private readonly IMediator _mediator;

    public Config Config => _configSingleton.Config;

    public float UpperTemperatureWarningLimit => Config.UpperTemperatureWarningLimit;
    public float LowerTemperatureWarningLimit => Config.LowerTemperatureWarningLimit;

    public int PredictionTimeIntervalSeconds => Config.PredictionTimeIntervalSeconds;

    public ConfigsManager(IConfigSingleton configSingleton, IMediator mediator)
    {
        _configSingleton = configSingleton;
        _mediator = mediator;
    }

    public async Task<bool> UpdateConfig(Config config)
    {
        var updateResult = await _mediator.Send(new UpdateConfigCommand(config));

        if (updateResult)
        {
            _configSingleton.UpdateConfig(config);
        }

        return updateResult;
    }
}