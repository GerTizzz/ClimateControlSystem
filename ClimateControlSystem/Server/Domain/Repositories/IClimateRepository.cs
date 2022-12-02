using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IClimateRepository
    {
        Task<PredictionData> GetLastPredictionAsync();

        Task<bool> AddAccuracyAsync(AccuracyData accuracy);

        Task<bool> AddPredictionAsync(PredictionData newMonitoringData);

        Task<List<MonitoringData>> GetClimateRecordsAsync(int amountOfRecords);
    }
}