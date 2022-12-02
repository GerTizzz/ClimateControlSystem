using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public interface IClimateService
    {
        Task<List<Prediction>> GetClimatePredictionsAsync(int countRecord);
    }
}
