using ClimateControlSystem.Server.Services.PredictionEngine.PredictionEngineResources;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IPredictionEngineService
    {
        Task<TensorPredictionResult> Predict(TensorPredictionRequest features);
    }
}
