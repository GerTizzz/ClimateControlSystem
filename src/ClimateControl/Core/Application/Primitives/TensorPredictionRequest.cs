using Microsoft.ML.Data;

namespace Application.Primitives
{
    public sealed class TensorPredictionRequest
    {
        [VectorType(12)]
        public float[] serving_default_input_1 { get; set; }
    }
}
