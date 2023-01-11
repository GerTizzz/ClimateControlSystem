using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IMicroclimateRepository
    {
        Task<PredictionResult> GetLastPredictionAsync();

        Task<bool> AddMicroclimateAsync(PredictionResult prediction, SensorsData monitoring, TemperatureEvent temperatureEvent, HumidityEvent humidityEvent);

        Task<IEnumerable<Monitoring>> GetMonitorings(int amountOfRecords);

        Task<bool> AddAccuracyAsync(AccuracyData accuracy);

        Task<IEnumerable<MicroclimateData>> GetMicroclimateDataAsync(int amountOfRecords);
    }
}