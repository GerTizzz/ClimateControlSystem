using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.Common;
using ClimateControlSystem.Shared.Enums;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IClimateRepository
    {
        Task<PredictionResult> GetLastPredictionAsync();

        Task<bool> AddClimateAsync(PredictionResult prediction, MonitoringData monitoring, IEnumerable<ClimateEventType> eventTypes, Config config);

        Task<IEnumerable<Prediction>> GetPredictionsWithAccuraciesAsync(int amountOfRecords);

        Task<bool> AddAccuracyAsync(AccuracyData accuracy);

        Task<IEnumerable<ClimateData>> GetClimateData(int amountOfRecords);
    }
}