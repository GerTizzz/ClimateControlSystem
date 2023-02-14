using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IMicroclimateRepository
    {
        Task<bool> SaveMonitoringAsync(MonitoringsEntity monitoring);

        Task<bool> SaveSensorsDataAsync(SensorsDataEntity sensorsData);

        Task<int> GetMicroclimatesCountAsync();

        Task<int> GetMonitoringsCountAsync();

        Task<PredictionsEntity> GetLastPredictionAsync();

        Task<IEnumerable<MonitoringsEntity>> GetMonitoringsAsync(int start, int count);

        Task<IEnumerable<MonitoringsEntity>> GetMonitoringsWithAccuraciesAsync(int start, int count);

        Task<IEnumerable<MonitoringsEntity>> GetMicroclimatesAsync(int start, int count);

        Task<IEnumerable<MonitoringsEntity>> GetMonitoringEventsAsync(int start, int count);
    }
}