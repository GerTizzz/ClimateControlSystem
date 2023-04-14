using Shared.Dtos;

namespace WebClient.Services.ForecastService
{
    public interface IForecastService
    {
        Task<long> GetForecastsCountAsync();

        Task<List<ForecastDto>> GetForecastsAsync(int start, int count);
    }
}
