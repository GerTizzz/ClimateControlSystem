using Microsoft.ML.Data;

namespace Application.Primitives;

public sealed class TensorPredictionRequest
{
    private const int NumberOfDataSets = 144;
    private const int MeasurementsPerDataSet = 3;

    public static int InputSize => NumberOfDataSets * MeasurementsPerDataSet;
    
    [VectorType(NumberOfDataSets, MeasurementsPerDataSet)]
    public float[] serving_default_lstm_input { get; set; }
}