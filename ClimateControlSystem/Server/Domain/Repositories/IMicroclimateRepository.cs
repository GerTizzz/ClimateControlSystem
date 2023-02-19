using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IMicroclimateRepository
    {
        Task<bool> SaveMonitoringAsync(MonitoringsEntity monitoring);

        Task<int> GetMicroclimatesCountAsync();

        Task<int> GetMonitoringsCountAsync();

        Task<PredictionsEntity?> TryGetLastPredictionAsync();

        Task<IEnumerable<MonitoringsEntity>> GetBaseMonitoringsAsync(RequestLimits requestLimits);

        Task<IEnumerable<MonitoringsEntity>> GetMonitoringsWithAccuraciesAsync(RequestLimits requestLimits);

        Task<IEnumerable<MonitoringsEntity>> GetMicroclimatesAsync(RequestLimits requestLimits);

        Task<IEnumerable<MonitoringsEntity>> GetMonitoringEventsAsync(RequestLimits requestLimits);
    }
}