using ClimateControl.Shared.Dtos;

namespace ClimateControl.WebClient.Services.MonitoringService
{
    public interface IMonitoringService
    {
        /// <returns>Amount of monitorings records count</returns>
        Task<long> GetCountAsync();

        /// <returns>Amount of monitorings events records count</returns>
        Task<long> GetEventsCountAsync();

        Task<List<BaseMonitoringDto>> GetBaseMonitoringsAsync(int start, int count);

        Task<List<MonitoringWithAccuracyDto>> GetMonitoringsWithAccuraciesAsync(int start, int count);

        Task<List<ForecastingDto>> GetForecastingsAsync(int start, int count);

        Task<List<MonitoringsEventsDto>> GetEventsAsync(int start, int count);
    }
}
