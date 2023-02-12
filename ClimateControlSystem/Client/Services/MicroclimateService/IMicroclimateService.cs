using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public interface IMicroclimateService
    {
        /// <returns>Amount of monitorings records count</returns>
        Task<int> GetMonitoringsCount();

        /// <returns>Amount of microclimates records count</returns>
        Task<int> GetMicroclimatesCount();

        Task<List<BaseMonitoringResponse>> GetBaseMonitoringsAsync(int start, int count);

        Task<List<MonitoringWithAccuraciesResponse>> GetMonitoringsWithAccuraciesAsync(int start, int count);

        /// <summary>
        /// Returns microclimate records
        /// </summary>
        /// <param name="offsetFromTheEnd">Number of records to skip from the end</param>
        Task<List<MicroclimateResponse>> GetMicroclimatesAsync(int offsetFromTheEnd, int count);

        Task<List<MonitoringEventsResponse>> GetMonitoringEventsAsync(int start, int count);
    }
}
