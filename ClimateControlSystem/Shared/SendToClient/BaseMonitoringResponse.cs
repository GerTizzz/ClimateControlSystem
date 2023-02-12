namespace ClimateControlSystem.Shared.SendToClient
{
    public record class BaseMonitoringResponse
    {
        public DateTimeOffset? MeasurementTime { get; set; }
        public float? PredictedTemperature { get; set; }
        public float? PredictedHumidity { get; set; }
        public float? MeasuredTemperature { get; set; }
        public float? MeasuredHumidity { get; set; }
    }
}
