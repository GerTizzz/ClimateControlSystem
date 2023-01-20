using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public interface IMicroclimateService
    {
        /// <returns>Amount of microclimates records count</returns>
        Task<int> GetMicroclimatesRecordsCount();

        Task<IReadOnlyCollection<MonitoringResponse>> GetMonitoringsDataAsync(int start, int count);

        /// <summary>
        /// Returns microclimate records
        /// </summary>
        /// <param name="offsetFromTheEnd">Number of records to skip from the end</param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<MicroclimateResponse>> GetMicroclimatesDataAsync(int offsetFromTheEnd, int count);

        Task<IReadOnlyCollection<TemperatureEventResponse>> GetTemperatureEventDataAsync(int start, int count);

        Task<IReadOnlyCollection<HumidityEventResponse>> GetHumidityEventDataAsync(int start, int count);
    }
}
