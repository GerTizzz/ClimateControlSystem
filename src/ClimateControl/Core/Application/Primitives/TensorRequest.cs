using Microsoft.ML.Data;

namespace Application.Primitives;

public sealed class TensorRequest
{    
    [VectorType(TensorSettings.NumberOfDataSets, TensorSettings.MeasurementsPerDataSet)]
    public float[] serving_default_lstm_input { get; set; }
}