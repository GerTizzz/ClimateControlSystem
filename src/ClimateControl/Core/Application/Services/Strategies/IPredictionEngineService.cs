using Application.Primitives;

namespace Application.Services.Strategies
{
    public interface IPredictionEngineService
    {
        Task<TensorPredictionResult> Predict(TensorPredictionRequest features);
    }
}
