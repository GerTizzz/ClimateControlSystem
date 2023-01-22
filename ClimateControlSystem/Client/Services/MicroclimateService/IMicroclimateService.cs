using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public interface IMicroclimateService
    {
        /// <returns>Amount of monitorings records count</returns>
        Task<int> GetMonitoringsCount();

        /// <returns>Amount of microclimates records count</returns>
        Task<int> GetMicroclimatesCount();

        Task<IReadOnlyCollection<MonitoringResponse>> GetMonitoringsAsync(int start, int count);

        /// <summary>
        /// Returns microclimate records
        /// </summary>
        /// <param name="offsetFromTheEnd">Number of records to skip from the end</param>
        Task<IReadOnlyCollection<MicroclimateResponse>> GetMicroclimatesAsync(int offsetFromTheEnd, int count);

        Task<IReadOnlyCollection<TemperatureEventResponse>> GetTemperatureEventsAsync(int start, int count);

        Task<IReadOnlyCollection<HumidityEventResponse>> GetHumidityEventsAsync(int start, int count);
    }
}
