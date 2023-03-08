using ClimateControl.Server.Resources.Infrastructure;
using ClimateControl.Server.Resources.Repository.TablesEntities;

namespace ClimateControl.Server.Infrastructure.Repositories
{
    public interface IMonitoringsRepository
    {
        Task<bool> SaveMonitoringAsync(MonitoringsEntity monitoring);

        Task<long> GetMonitoringsCountAsync();

        Task<long> GetMicroclimatesEventsCountAsync();

        Task<PredictionsEntity?> TryGetLastPredictionAsync();

        Task<IEnumerable<MonitoringsEntity>> GetBaseMonitoringsAsync(RequestLimits requestLimits);

        Task<IEnumerable<MonitoringsEntity>> GetMonitoringsWithAccuraciesAsync(RequestLimits requestLimits);

        Task<IEnumerable<MonitoringsEntity>> GetMicroclimatesAsync(RequestLimits requestLimits);

        Task<IEnumerable<MonitoringsEntity>> GetMicroclimatesEventsAsync(RequestLimits requestLimits);
    }
}