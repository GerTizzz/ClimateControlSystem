using ClimateControlSystem.Shared.Responses;

namespace ClimateControlSystem.Client.Services.MicroclimateService
{
    public interface IMicroclimateService
    {
        /// <returns>Amount of monitorings records count</returns>
        Task<int> GetMonitoringsCount();

        /// <returns>Amount of microclimates records count</returns>
        Task<int> GetMicroclimatesCount();

        Task<List<BaseMonitoringDto>> GetBaseMonitoringsAsync(int start, int count);

        Task<List<MonitoringWithAccuracyDto>> GetMonitoringsWithAccuraciesAsync(int start, int count);

        /// <summary>
        /// Returns microclimate records
        /// </summary>
        /// <param name="offsetFromTheEnd">Number of records to skip from the end</param>
        Task<List<ForecastingDto>> GetMicroclimatesAsync(int offsetFromTheEnd, int count);

        Task<List<MonitoringsEventsDto>> GetMonitoringEventsAsync(int start, int count);
    }
}
