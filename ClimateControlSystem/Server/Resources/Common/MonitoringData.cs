namespace ClimateControlSystem.Server.Resources.Common
{
    public sealed class MonitoringData
    {
        public DateTimeOffset MeasurementTime { get; set; }
        public float CurrentRealTemperature { get; set; }
        public float CurrentRealHumidity { get; set; }
        public float PredictedFutureTemperature { get; set; }
        public float PredictedFutureHumidity { get; set; }
        public float? PredictedTemperatureAccuracy { get; set; }
        public float? PredictedHumidityAccuracy { get; set; }
        public TemperatureEvent? TemperatureEvent { get; set; }
        public HumidityEvent? HumidityEvent { get; set; }
    }
}
