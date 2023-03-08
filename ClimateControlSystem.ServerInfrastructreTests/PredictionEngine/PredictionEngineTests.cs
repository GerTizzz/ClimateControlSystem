using ClimateControl.Server.Resources.PredictionEngine;
using ClimateControl.Server.Services;
using System.Xml.Linq;

namespace Server.Tests.PredictionEngine
{
    public sealed class PredictionEngineTests
    {
        [Test]
        public async Task CheckPrediction()
        {            
            //arrange
            string modelPath = "C:\\Users\\miste\\Desktop\\Diplom\\DC Climate Control System\\ClimateControlSystem\\ClimateControlSystem\\Server\\Resources\\PredictionEngine\\MlModel\\keras model";

            PredictionEngineService predictionEngine = new PredictionEngineService(modelPath);

            string data = "11.3;1322.552;31.1;15.5;13.64;83;-14;-14.83;2.67;60;-16.84;22.47;15.71;13.83";

            float[] features = data.Replace('.', ',').Split(';').Select(float.Parse).ToArray();

            TensorPredictionRequest request = new TensorPredictionRequest()
            {
                serving_default_input_1 = features
            };

            var prediction = await predictionEngine.Predict(request);

            string print = "Температура воздуха внутри ЦОД: " +
                prediction.StatefulPartitionedCall[0] +
                Environment.NewLine +
                "Влажность воздуха внутри ЦОД: " +
                prediction.StatefulPartitionedCall[1];

            Console.WriteLine(print);

            Assert.IsNotNull(prediction);
        }
    }
}
