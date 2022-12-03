using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IClimateRepository
    {
        Task<PredictionResult> GetLastPredictionAsync();

        Task<bool> AddPredictionAsync(PredictionResult prediction, MonitoringData monitoring, AccuracyData accuracy);

        Task<List<Prediction>> GetPredictionsWithAccuraciesAsync(int amountOfRecords);
    }
}