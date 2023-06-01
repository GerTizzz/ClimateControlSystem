using Domain.Entities;

namespace Domain.Services;

public interface IConfigsManager
{
    Config Config { get; }
    float UpperTemperatureWarningLimit { get; }
    float LowerTemperatureWarningLimit { get; }

    int PredictionTimeIntervalSeconds { get; }

    Task<bool> UpdateConfig(Config config);
}