namespace ClimateControlSystem.Shared.SendToClient
{
    public record class BaseMonitoringResponse
    {
        public DateTimeOffset? MeasurementTime { get; set; }
        public float? TemperaturePredictionForFuture { get; set; }
        public float? HumidityPredictionForFuture { get; set; }
        public float? MeasuredTemperature { get; set; }
        public float? MeasuredHumidity { get; set; }
    }
}
