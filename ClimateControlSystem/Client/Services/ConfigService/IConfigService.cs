using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Client.Services.ConfigService
{
    public interface IConfigService
    {
        Task<ConfigResponse> GetConfigAsync();

        Task<bool> UpdateConfigAsync(ConfigResponse config);
    }
}
