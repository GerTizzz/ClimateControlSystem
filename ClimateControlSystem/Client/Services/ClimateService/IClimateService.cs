using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public interface IClimateService
    {
        Task<List<Prediction>> GetPredictionsAsync(int countRecord);
        Task<List<ClimateData>> GetClimatesDataAsync(int countRecord);
    }
}
