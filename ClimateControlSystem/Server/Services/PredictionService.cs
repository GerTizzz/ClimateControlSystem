using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using Tensorflow.Contexts;

namespace ClimateControlSystem.Server.Services
{
    public sealed class PredictionService : IPredictionService
    {
        private readonly PredictionEngine<TensorData, OutputScores> _predictionEgine;

        public PredictionService(string modelLocation)
        {
            _predictionEgine = CreatePredictionEgine(modelLocation);
        }

        public float[] Predict(float[] inputData)
        {
            //45f, 5266.8f, 49.2f, 23.3f, 20.19f, 92.56f, -3.06f, -3.99f, 2.44f, 225f, -3.61f, 19.39f
            //36.1f, 4225.144f, 50.4f, 22.79f, 20.49f, 92.89f, -3.09f, -4.02f, 2.11f, 225, -3.95f, 18.38f
            TensorData data = GetTensorData(inputData);

            OutputScores predict = _predictionEgine.Predict(data);

            return predict.StatefulPartitionedCall.ToArray();
        }

        private PredictionEngine<TensorData, OutputScores> CreatePredictionEgine(string modelLocation)
        {
            MLContext mlContext = new MLContext();

            TensorFlowModel model = mlContext.Model.LoadTensorFlowModel(modelLocation);

            TensorFlowEstimator pipeline = model.ScoreTensorFlowModel(
                new[] { "StatefulPartitionedCall" },
                new[] { "serving_default_input_1" });

            TensorFlowTransformer transformer = pipeline.Fit(CreateEmptyDataView(mlContext));

            return mlContext.Model.CreatePredictionEngine<TensorData, OutputScores>(transformer);
        }

        private static IDataView CreateEmptyDataView(MLContext mlContext)
        {
            IEnumerable<TensorData> enumerableData = new List<TensorData>();
            return mlContext.Data.LoadFromEnumerable(enumerableData);
        }

        private static TensorData GetTensorData(float[] inputData)
        {
            return new TensorData() { serving_default_input_1 = inputData };
        }
    }
    public class TensorData
    {
        [VectorType(12)]
        public float[] serving_default_input_1 { get; set; }
    }

    class OutputScores
    {
        public float[] StatefulPartitionedCall { get; set; }
    }
}
