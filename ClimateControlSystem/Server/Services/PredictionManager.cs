using ClimateControlSystem.Shared;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;

namespace ClimateControlSystem.Server.Services
{
    public sealed class PredictionManager : IPredictionManager
    {
        private readonly PredictionEngine<TensorPredictionRequest, TensorPredictionResult> _predictionEgine;

        public PredictionManager(string modelLocation)
        {
            _predictionEgine = CreatePredictionEgine(modelLocation);
        }

        public PredictionResult Predict(PredictionRequest inputData)
        {
            //45f, 5266.8f, 49.2f, 23.3f, 20.19f, 92.56f, -3.06f, -3.99f, 2.44f, 225f, -3.61f, 19.39f
            //36.1f, 4225.144f, 50.4f, 22.79f, 20.49f, 92.89f, -3.09f, -4.02f, 2.11f, 225, -3.95f, 18.38f
            TensorPredictionRequest features = ConvertPredictionRequestToTensorDataFeatures(inputData);

            TensorPredictionResult prediction = _predictionEgine.Predict(features);

            PredictionResult predictionResult = ConvertTensorDataResultToPredictionResult(prediction, features);

            return predictionResult;
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

        private TensorPredictionRequest ConvertPredictionRequestToTensorDataFeatures(PredictionRequest inputData)
        {
            float[] features = new float[] 
            {
                inputData.ClusterLoad,
                inputData.CpuUsage,
                inputData.ClusterTemperature,
                inputData.PreviousTemperature,
                inputData.PreviousHumidity,
                inputData.AirHumidityOutside,
                inputData.AirDryTemperatureOutside,
                inputData.AirWetTemperatureOutside,
                inputData.WindSpeed,
                inputData.WindDirection,
                inputData.WindEnthalpy,
                inputData.MeanCoolingValue
            };

            return new TensorPredictionRequest() { serving_default_input_1 = features };
        }

        private PredictionResult ConvertTensorDataResultToPredictionResult(TensorPredictionResult labels, TensorPredictionRequest features)
        {
            PredictionResult result = new PredictionResult()
            {
                ClusterLoad = features.serving_default_input_1[0],
                CpuUsage = features.serving_default_input_1[1],
                ClusterTemperature = features.serving_default_input_1[2],
                PreviousTemperature = features.serving_default_input_1[3],
                PreviousHumidity = features.serving_default_input_1[4],
                AirHumidityOutside = features.serving_default_input_1[5],
                AirDryTemperatureOutside = features.serving_default_input_1[6],
                AirWetTemperatureOutside = features.serving_default_input_1[7],
                WindSpeed = features.serving_default_input_1[8],
                WindDirection = features.serving_default_input_1[9],
                WindEnthalpy = features.serving_default_input_1[10],
                MeanCoolingValue = features.serving_default_input_1[11],
                PredictedTemperature = labels.StatefulPartitionedCall[0],
                PredictedHumidity = labels.StatefulPartitionedCall[1]
            };

            return result;
        }
    }

    public class TensorPredictionRequest
    {
        [VectorType(12)]
        public float[] serving_default_input_1 { get; set; }
    }

    class TensorPredictionResult
    {
        public float[] StatefulPartitionedCall { get; set; }
    }
}
