using ClimateControlSystem.Shared.Responses;

namespace ClimateControlSystem.Client.Services.MonitoringService
{
    public interface IMonitoringService
    {
        /// <returns>Amount of monitorings records count</returns>
        Task<long> GetCountAsync();

        /// <returns>Amount of monitorings events records count</returns>
        Task<long> GetEventsCountAsync();

        Task<IEnumerable<BaseMonitoringDto>> GetBaseMonitoringsAsync(int start, int count);

        Task<IEnumerable<MonitoringWithAccuracyDto>> GetMonitoringsWithAccuraciesAsync(int start, int count);

        Task<IEnumerable<ForecastingDto>> GetForecastingsAsync(int start, int count);

        Task<IEnumerable<MonitoringsEventsDto>> GetEventsAsync(int start, int count);
    }
}
