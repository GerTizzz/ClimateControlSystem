using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IMonitoringDataRepository
    {
        Task<bool> AddPredictionAsync(MonitoringData newRecord);

        Task<List<MonitoringData>> GetClimateRecordsAsync(int amountOfRecords);

        Task<MonitoringData> GetLastPredictionAsync();

        Task<bool> UpdatePredictionAccuracies(MonitoringData updatedData);
    }
}