using ClimateControl.Shared.Dtos;

namespace ClimateControl.WebClient.Services.ConfigService
{
    public interface IConfigService
    {
        Task<ConfigsDto> GetConfigAsync();

        Task<bool> UpdateConfigAsync(ConfigsDto config);
    }
}
