using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IMicroclimateRepository
    {
        Task<PredictionResult> GetLastPredictionAsync();

        Task<bool> AddSensorsDataAsync(SensorsData monitoring);
        
        Task<bool> AddAccuracyAsync(PredictionAccuracy accuracy);

        Task<bool> AddPredictionAsync(PredictionResult prediction, TemperatureEvent temperatureEvent, HumidityEvent humidityEvent);

        Task<int> GetMicroclimatesCountAsync();

        Task<int> GetMonitoringsCountAsync();

        Task<MonitoringResponse[]> GetMonitoringsAsync(int start, int count);

        Task<MicroclimateResponse[]> GetMicroclimatesAsync(int start, int count);

        Task<TemperatureEvent[]> GetTemperatureEventsAsync(int start, int count);

        Task<HumidityEvent[]> GetHumidityEventsAsync(int start, int count);
    }
}