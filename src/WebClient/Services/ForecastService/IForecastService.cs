using Shared.Dtos;

namespace WebClient.Services.ForecastService
{
    public interface IForecastService
    {
        Task<long> GetForecastsCountAsync();

        Task<long> GetWarningsCountAsync();

        Task<List<ForecastDto>> GetForecastsAsync(int start, int count);

        Task<List<WarningDto>> GetWarningsAsync(int start, int count);
    }
}
