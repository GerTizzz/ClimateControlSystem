using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Client.Services.ConfigService
{
    public interface IConfigService
    {
        Task<Config> GetConfigAsync();

        Task<bool> UpdateConfigAsync(Config config);
    }
}
