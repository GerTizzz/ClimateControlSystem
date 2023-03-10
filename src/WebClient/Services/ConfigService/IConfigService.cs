using Shared.Dtos;

namespace WebClient.Services.ConfigService
{
    public interface IConfigService
    {
        Task<ConfigsDto> GetConfigAsync();

        Task<bool> UpdateConfigAsync(ConfigsDto config);
    }
}
