namespace ClimateControlSystem.Server.Resources.Common
{
    public class Monitoring
    {
        public DateTimeOffset? MeasurementTime { get; set; }
        public float? MeasuredTemperature { get; set; }
        public float? MeasuredHumidity { get; set; }
        public float? PredictedTemperature { get; set; }
        public float? PredictedHumidity { get; set; }
        /// <remarks> Can be null in case if prediction value is in allowed interval </remarks>
        public TemperatureEvent? TemperaturePredictionEvent { get; set; }
        /// <remarks> Can be null in case if prediction value is in allowed interval </remarks>
        public HumidityEvent? HumidityPredictionEvent { get; set; }
        /// <remarks> Can be null in case of first monitoring, when there is no any previous data to calculate accuracy </remarks>
        public float? PredictedTemperatureAccuracy { get; set; }
        /// <remarks> Can be null in case of first monitoring, when there is no any previous data to calculate accuracy </remarks>
        public float? PredictedHumidityAccuracy { get; set; }
    }
}
