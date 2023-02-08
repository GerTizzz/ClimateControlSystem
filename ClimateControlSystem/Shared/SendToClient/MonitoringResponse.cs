namespace ClimateControlSystem.Shared.SendToClient
{
    public record class MonitoringResponse
    {
        public DateTimeOffset MeasurementTime { get; set; }
        public float MeasuredTemperature { get; set; }
        public float MeasuredHumidity { get; set; }
        public float TemperaturePredictionForFuture { get; set; }
        public float HumidityPredictionForFuture { get; set; }
        public float? PreviousTemperaturePredictionAccuracy { get; set; }
        public float? PreviousHumidityPredicitionAccuracy { get; set; }
        public TemperatureEventResponse? TemperaturePredictionEvent { get; set; }
        public HumidityEventResponse? HumidityPredictionEvent { get; set; }
    }
}
