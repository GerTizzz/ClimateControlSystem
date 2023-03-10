using Microsoft.ML;
using Microsoft.ML.Transforms;
using WebApi.Infrastructure.Services;
using WebApi.Resources.PredictionEngine;

namespace WebApi.Services
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
