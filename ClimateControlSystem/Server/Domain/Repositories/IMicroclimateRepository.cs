using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IMicroclimateRepository
    {
        Task<bool> SaveMonitoringAsync(Monitoring monitoring);

        Task<bool> SaveOrUpdateSensorsDataAsync(SensorsData sensorsData);

        Task<int> GetMicroclimatesCountAsync();

        Task<int> GetMonitoringsCountAsync();

        Task<Prediction> GetLastPredictionAsync();

        Task<IEnumerable<Monitoring>> GetMonitoringsAsync(int start, int count);

        Task<IEnumerable<Monitoring>> GetMonitoringsWithAccuraciesAsync(int start, int count);

        Task<IEnumerable<MicroclimateResponse>> GetMicroclimatesAsync(int start, int count);

        Task<IEnumerable<Monitoring>> GetMonitoringEventsAsync(int start, int count);
    }
}