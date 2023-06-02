using Application.Primitives;

namespace Application.Services.Strategies;

public interface IPredictionEngine
{
    Task<TensorResult> Predict(TensorRequest features);
}