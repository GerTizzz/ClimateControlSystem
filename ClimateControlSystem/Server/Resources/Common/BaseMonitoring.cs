namespace ClimateControlSystem.Server.Resources.Common
{
    public class BaseMonitoring
    {
        public DateTimeOffset? MeasurementTime { get; set; }
        public float? MeasuredTemperature { get; set; }
        public float? MeasuredHumidity { get; set; }
        public float? TemperaturePredictionForFuture { get; set; }
        public float? HumidityPredictionForFuture { get; set; }
        ///// <remarks> Can be null in case of first monitoring, when there is no any previous data to calculate accuracy </remarks>
        //public float? PreviousTemperaturePredictionAccuracy { get; set; }
        ///// <remarks> Can be null in case of first monitoring, when there is no any previous data to calculate accuracy </remarks>
        //public float? PreviousHumidityPredicitionAccuracy { get; set; }
    }
}
