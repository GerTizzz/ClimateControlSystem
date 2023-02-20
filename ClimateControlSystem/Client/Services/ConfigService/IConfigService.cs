using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Client.Services.ConfigService
{
    public interface IConfigService
    {
        Task<ConfigsDTO> GetConfigAsync();

        Task<bool> UpdateConfigAsync(ConfigsDTO config);
    }
}
