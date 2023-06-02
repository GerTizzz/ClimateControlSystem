using Application.Primitives;
using Application.Services.Strategies;
using Microsoft.ML;

namespace Application.Services.Implementations;

public sealed class PredictionEngine : IPredictionEngine
{
    private readonly PredictionEngine<TensorRequest, TensorResult> _predictionEgine;

    public PredictionEngine(string modelLocation)
    {
        _predictionEgine = CreatePredictionEgine(modelLocation);
    }

    public Task<TensorResult> Predict(TensorRequest features)
    {
        var labels = _predictionEgine.Predict(features);

        return Task.FromResult(labels);
    }

    private PredictionEngine<TensorRequest, TensorResult> CreatePredictionEgine(string modelLocation)
    {
        var mlContext = new MLContext();

        var model = mlContext.Model.LoadTensorFlowModel(modelLocation);

        var pipeline = model.ScoreTensorFlowModel(
            new[] { "StatefulPartitionedCall" },
            new[] { "serving_default_lstm_input" });

        var transformer = pipeline.Fit(CreateEmptyDataView(mlContext));

        return mlContext.Model.CreatePredictionEngine<TensorRequest, TensorResult>(transformer);
    }

    private static IDataView CreateEmptyDataView(MLContext mlContext)
    {
        return mlContext.Data.LoadFromEnumerable(Enumerable.Empty<TensorRequest>());
    }
}