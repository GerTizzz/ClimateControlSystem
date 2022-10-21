using ClimateControlSystem.Server.Resources;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IPredictionEngineService
    {
        PredictionResult Predict(PredictionRequest inputData);
    }
}
