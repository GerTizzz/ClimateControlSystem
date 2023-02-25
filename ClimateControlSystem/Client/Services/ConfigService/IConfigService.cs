using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Client.Services.ConfigService
{
    public interface IConfigService
    {
        Task<ConfigsDto> GetConfigAsync();

        Task<bool> UpdateConfigAsync(ConfigsDto config);
    }
}
