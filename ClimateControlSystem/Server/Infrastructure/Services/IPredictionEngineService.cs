using ClimateControl.Server.Resources.PredictionEngine;

namespace ClimateControl.Server.Infrastructure.Services
{
    public interface IPredictionEngineService
    {
        Task<TensorPredictionResult> Predict(TensorPredictionRequest features);
    }
}
