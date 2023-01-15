using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IMicroclimateRepository
    {
        Task<PredictionResultData> GetLastPredictionAsync();

        Task<bool> AddMicroclimateAsync(PredictionResultData prediction, SensorsData monitoring, TemperatureEventData temperatureEvent, HumidityEventData humidityEvent);
        
        Task<bool> AddAccuracyAsync(AccuracyData accuracy);

        Task<int> GetMicroclimatesCount();

        Task<MonitoringResponse[]> GetMonitorings(int start, int count);

        Task<MicroclimateResponse[]> GetMicroclimateDataAsync(int start, int count);

        Task<TemperatureEventData[]> GetTemperatureEventsAsync(int start, int count);

        Task<HumidityEventData[]> GetHumidityEventsAsync(int start, int count);
    }
}