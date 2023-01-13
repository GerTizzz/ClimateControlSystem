namespace ClimateControlSystem.Shared
{
    public readonly struct MonitoringResponse
    {
        public DateTimeOffset MeasurementTime { get; init; }
        public float PredictedFutureTemperature { get; init; }
        public float PredictedFutureHumidity { get; init; }
        public float? CurrentRealTemperature { get; init; }
        public float? CurrentRealHumidity { get; init; }      
        public float? PredictedTemperatureAccuracy { get; init; }
        public float? PredictedHumidityAccuracy { get; init; }
    }
}
