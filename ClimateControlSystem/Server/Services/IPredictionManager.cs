using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Services
{
    public interface IPredictionManager
    {
        PredictionResult Predict(PredictionRequest inputData);
    }
}
