namespace ClimateControlSystem.Server.Resources.Common
{
    public sealed class Accuracy
    {
        public float PredictedTemperatureAccuracy { get; set; }
        public float PredictedHumidityAccuracy { get; set; }

        public Accuracy Clone()
        {
            var clone = new Accuracy()
            {
                PredictedTemperatureAccuracy = PredictedTemperatureAccuracy,
                PredictedHumidityAccuracy = PredictedHumidityAccuracy
            };

            return clone;
        }
    }
}
