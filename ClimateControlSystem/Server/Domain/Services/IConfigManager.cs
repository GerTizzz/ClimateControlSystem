using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IConfigManager
    {
        float TemperatureLimit { get; }

        float HumidityLimit { get; }

        Task<Config> GetConfigAsync();

        Task<bool> UpdateConfig(Config config);
    }
}