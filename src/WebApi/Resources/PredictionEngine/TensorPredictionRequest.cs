using Microsoft.ML.Data;

namespace WebApi.Resources.PredictionEngine
{
    public sealed class TensorPredictionRequest
    {
        [VectorType(12)]
        public float[] serving_default_input_1 { get; set; }
    }
}
