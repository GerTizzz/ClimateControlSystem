using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IConfigManager
    {
        float TemperatureLimit { get; }

        float HumidityLimit { get; }

        float UpperTemperatureWarningLimit { get; }
        float LowerTemperatureWarningLimit { get; }
        float UpperTemperatureCriticalLimit { get; }
        float LowerTemperatureCriticalLimit { get; }

        float UpperHumidityWarningLimit { get; }
        float LowerHumidityWarningLimit { get; }
        float UpperHumidityCriticalLimit { get; }
        float LowerHumidityCriticalLimit { get; }

        Task<Config> GetConfigAsync();

        Task<bool> UpdateConfig(Config config);
    }
}