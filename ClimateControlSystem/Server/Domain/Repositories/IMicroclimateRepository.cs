using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IMicroclimateRepository
    {
        Task<PredictionResultData> GetLastPredictionAsync();

        Task<bool> AddMicroclimateAsync(PredictionResultData prediction, SensorsData monitoring, TemperatureEventData temperatureEvent, HumidityEventData humidityEvent);

        Task<IReadOnlyCollection<MonitoringResponse>> GetMonitorings(int count);

        Task<bool> AddAccuracyAsync(AccuracyData accuracy);

        Task<IReadOnlyCollection<MicroclimateResponse>> GetMicroclimateDataAsync(int count);

        Task<IReadOnlyCollection<TemperatureEventData>> GetTemperatureEventsAsync(int start, int count);

        Task<IReadOnlyCollection<HumidityEventData>> GetHumidityEventsAsync(int start, int count);
    }
}