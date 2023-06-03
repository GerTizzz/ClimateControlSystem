using Shared.Dtos;

namespace WebClient.Services.ForecastService
{
    public interface IForecastService
    {
        Task<long> GetForecastsCountAsync();

        Task<ForecastDto?> GetForecastAsync(int number);

        Task<List<ForecastDto>> GetForecastsAsync(int start, int count);
    }
}
