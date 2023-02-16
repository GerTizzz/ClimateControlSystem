using ClimateControlSystem.Server.Resources.Repository.TablesEntities;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IMicroclimateRepository
    {
        Task<bool> SaveMonitoringAsync(MonitoringsEntity monitoring);

        Task<int> GetMicroclimatesCountAsync();

        Task<int> GetMonitoringsCountAsync();

        Task<ActualDataEntity?> TryGetLastActualDataAsync();

        Task<IEnumerable<MonitoringsEntity>> GetMonitoringsAsync(int start, int count);

        Task<IEnumerable<MonitoringsEntity>> GetMonitoringsWithAccuraciesAsync(int start, int count);

        Task<IEnumerable<MonitoringsEntity>> GetMicroclimatesAsync(int start, int count);

        Task<IEnumerable<MonitoringsEntity>> GetMonitoringEventsAsync(int start, int count);
    }
}