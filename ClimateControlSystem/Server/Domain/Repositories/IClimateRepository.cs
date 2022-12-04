using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.Enums;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IClimateRepository
    {
        Task<PredictionResult> GetLastPredictionAsync();

        Task<bool> AddPredictionAsync(PredictionResult prediction, MonitoringData monitoring, AccuracyData accuracy, ClimateEventType eventType);

        Task<List<Prediction>> GetPredictionsWithAccuraciesAsync(int amountOfRecords);
    }
}