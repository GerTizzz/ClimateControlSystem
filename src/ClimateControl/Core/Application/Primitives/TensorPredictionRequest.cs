using Microsoft.ML.Data;

namespace Application.Primitives;

public sealed class TensorPredictionRequest
{
    [VectorType(144, 3)]
    public float[][] serving_default_lstm_input { get; set; }
}