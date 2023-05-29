using Application.Primitives;
using Application.Services.Strategies;
using Microsoft.ML;

namespace Application.Services.Implementations;

public sealed class PredictionEngine : IPredictionEngine
{
    private readonly PredictionEngine<TensorPredictionRequest, TensorPredictionResult> _predictionEgine;

    public PredictionEngine(string modelLocation)
    {
        _predictionEgine = CreatePredictionEgine(modelLocation);
    }

    public Task<TensorPredictionResult> Predict(TensorPredictionRequest features)
    {
        var labels = _predictionEgine.Predict(features);

        return Task.FromResult(labels);
    }

    private PredictionEngine<TensorPredictionRequest, TensorPredictionResult> CreatePredictionEgine(string modelLocation)
    {
        var mlContext = new MLContext();

        var model = mlContext.Model.LoadTensorFlowModel(modelLocation);

        var pipeline = model.ScoreTensorFlowModel(
            new[] { "StatefulPartitionedCall" },
            new[] { "serving_default_lstm_input" });

        var transformer = pipeline.Fit(CreateEmptyDataView(mlContext));

        return mlContext.Model.CreatePredictionEngine<TensorPredictionRequest, TensorPredictionResult>(transformer);
    }

    private static IDataView CreateEmptyDataView(MLContext mlContext)
    {
        IEnumerable<TensorPredictionRequest> enumerableData = new List<TensorPredictionRequest>();
        return mlContext.Data.LoadFromEnumerable(enumerableData);
    }
}