using Application.Primitives;

namespace Application.Services.Strategies;

public interface IPredictionEngine
{
    Task<TensorPredictionResult> Predict(TensorPredictionRequest features);
}