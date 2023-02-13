namespace ClimateControlSystem.Shared.SendToClient
{
    public record class MonitoringEventsResponse
    {
        public DateTimeOffset? Time { get; set; }
        public float? TemperatureValue { get; init; }
        public float? HumidityValue { get; init; }
    }
}
