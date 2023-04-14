using Shared.Dtos;

namespace WebClient.Services.WarningService;

public interface IWarningService
{
    Task<long> GetWarningsCountAsync();

    Task<List<WarningDto>> GetWarningsAsync(int start, int count);
}