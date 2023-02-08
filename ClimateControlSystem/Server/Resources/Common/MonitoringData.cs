namespace ClimateControlSystem.Server.Resources.Common
{
    public sealed class MonitoringData
    {
        public DateTimeOffset MeasurementTime { get; set; }
        public float MeasuredTemperature { get; set; }
        public float MeasuredHumidity { get; set; }
        public float TemperaturePredictionForFuture { get; set; }
        public float HumidityPredictionForFuture { get; set; }
        /// <remarks> Can be null in case of first monitoring, when there is no any previous data to calculate accuracy </remarks>
        public float? PredviousTemperaturePredictionAccuracy { get; set; }
        /// <remarks> Can be null in case of first monitoring, when there is no any previous data to calculate accuracy </remarks>
        public float? PreviousHumidityPredicitionAccuracy { get; set; }
        /// <remarks> Can be null in case if prediction value is in allowed interval </remarks>
        public TemperatureEvent? TemperaturePredictionEvent { get; set; }
        /// <remarks> Can be null in case if prediction value is in allowed interval </remarks>
        public HumidityEvent? HumidityPredictionEvent { get; set; }
    }
}
