using ClimateControlSystem.Server.Resources.Common;
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

        Task<IEnumerable<Monitoring>> GetMonitoringsAsync(int start, int count);

        Task<IEnumerable<Monitoring>> GetMonitoringsWithAccuraciesAsync(int start, int count);

        Task<IEnumerable<MicroclimateResponse>> GetMicroclimatesAsync(int start, int count);

        Task<IEnumerable<Monitoring>> GetMonitoringEventsAsync(int start, int count);
    }
}