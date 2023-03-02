using ClimateControlSystem.Server.Resources.Domain;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IConfigManager
    {
        Config Config { get; }
        float UpperTemperatureWarningLimit { get; }
        float LowerTemperatureWarningLimit { get; }

        float UpperHumidityWarningLimit { get; }
        float LowerHumidityWarningLimit { get; }

        int PredictionTimeIntervalSeconds { get; }

        Task<bool> UpdateConfig(Config config);
    }
}