using WebApi.Resources.PredictionEngine;
using WebApi.Services;

namespace WebApiTests.PredictionEngine
{
    public sealed class PredictionEngineTests
    {
        private const string FeaturesData = "11.3;1322.552;31.1;15.5;13.64;83;-14;-14.83;2.67;60;-16.84;22.47;15.71;13.83";

        [Test]
        public async Task CheckPrediction()
        {            
            //arrange
            var modelLocation = string.Join("\\", Directory.GetCurrentDirectory()
                .Split('\\')
                .TakeWhile(str => str != "tests")) + "\\mlModel";

            var predictionEngine = new PredictionEngineService(modelLocation);
            
            var features = FeaturesData.Replace('.', ',').Split(';').Select(float.Parse).ToArray();

            var request = new TensorPredictionRequest()
            {
                serving_default_input_1 = features
            };

            var prediction = await predictionEngine.Predict(request);

            var print = "Температура воздуха внутри ЦОД: " +
                        prediction.StatefulPartitionedCall[0] +
                        Environment.NewLine +
                        "Влажность воздуха внутри ЦОД: " +
                        prediction.StatefulPartitionedCall[1];

            Console.WriteLine(print);

            Assert.That(prediction, Is.Not.Null);
        }
    }
}
