using WebApi.Resources.PredictionEngine;

namespace WebApi.Infrastructure.Services
{
    public interface IPredictionEngineService
    {
        Task<TensorPredictionResult> Predict(TensorPredictionRequest features);
    }
}
