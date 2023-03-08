using ClimateControl.Server.Infrastructure.Services;
using ClimateControl.Server.Resources.PredictionEngine;
using Microsoft.ML;
using Microsoft.ML.Transforms;

namespace ClimateControl.Server.Services
{
    public sealed class PredictionEngineService : IPredictionEngineService
    {
        private readonly PredictionEngine<TensorPredictionRequest, TensorPredictionResult> _predictionEgine;

        public PredictionEngineService(string modelLocation)
        {
            _predictionEgine = CreatePredictionEgine(modelLocation);
        }

        public Task<TensorPredictionResult> Predict(TensorPredictionRequest features)
        {
            TensorPredictionResult labels = _predictionEgine.Predict(features);

            return Task.FromResult(labels);
        }

        private PredictionEngine<TensorPredictionRequest, TensorPredictionResult> CreatePredictionEgine(string modelLocation)
        {
            MLContext mlContext = new MLContext();

            TensorFlowModel model = mlContext.Model.LoadTensorFlowModel(modelLocation);

            TensorFlowEstimator pipeline = model.ScoreTensorFlowModel(
                new[] { "StatefulPartitionedCall" },
                new[] { "serving_default_input_1" });

            TensorFlowTransformer transformer = pipeline.Fit(CreateEmptyDataView(mlContext));

            return mlContext.Model.CreatePredictionEngine<TensorPredictionRequest, TensorPredictionResult>(transformer);
        }

        private IDataView CreateEmptyDataView(MLContext mlContext)
        {
            IEnumerable<TensorPredictionRequest> enumerableData = new List<TensorPredictionRequest>();
            return mlContext.Data.LoadFromEnumerable(enumerableData);
        }
    }
}
