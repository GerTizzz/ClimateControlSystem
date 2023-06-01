using Application.MediatR.ConfigsRepository;
using Domain.Singletons;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Singletons;

public sealed class ConfigSingleton : IConfigSingleton
{
    private readonly object _lock;

    public float UpperTemperatureLimit => Config.UpperTemperatureWarningLimit;
    
    public float LowerTemperatureLimit => Config.LowerTemperatureWarningLimit;
    
    public float UpLimitOk { get; private set; }
    
    public float LowLimitOk { get; private set; }
    public Config Config { get; private set; }

    public ConfigSingleton(IServiceScopeFactory scopeFactory)
    {
        _lock = new object();

        using var scope = scopeFactory.CreateScope();
        
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        Config = mediator.Send(new GetConfigQuery()).Result;

        RecalculateOkRange();
    }
    
    public Task<bool> UpdateConfig(Config config)
    {
        var updateResult = false;

        try
        {
            lock (_lock)
            {
                Config = config;

                RecalculateOkRange();
            }

            updateResult = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Task.FromResult(updateResult);
    }

    private void RecalculateOkRange()
    {
        var median = (UpperTemperatureLimit - LowerTemperatureLimit) / 2 + LowerTemperatureLimit;

        UpLimitOk = median + 0.5f;
        LowLimitOk = median - 0.5f;
    }
}