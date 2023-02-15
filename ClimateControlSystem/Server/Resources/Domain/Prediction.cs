namespace ClimateControlSystem.Server.Resources.Common
{
    public class Prediction
    {
        public float PredictedTemperature { get; set; }
        public float PredictedHumidity { get; set; }

        public Prediction Clone()
        {
            var clone = new Prediction()
            {
                PredictedTemperature = PredictedTemperature,
                PredictedHumidity = PredictedHumidity
            };

            return clone;
        }
    }
}
