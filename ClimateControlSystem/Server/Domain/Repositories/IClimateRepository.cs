using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.Common;
using ClimateControlSystem.Shared.Enums;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IClimateRepository
    {
        Task<PredictionResult> GetLastPredictionAsync();

        Task<bool> AddPredictionAsync(PredictionResult prediction, MonitoringData monitoring, AccuracyData accuracy, List<ClimateEventType> eventTypes, Config config);

        Task<List<Prediction>> GetPredictionsWithAccuraciesAsync(int amountOfRecords);
    }
}