using ClimateControlSystem.Server.Resources;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IPredictionService
    {
        public Task<PredictionResult> GetPrediction(PredictionRequest incomingRequest);
    }
}
