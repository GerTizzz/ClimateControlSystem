namespace ClimateControlSystem.Shared.SendToClient
{
    public record class MonitoringEventsDTO
    {
        public DateTimeOffset? Time { get; set; }
        public float? TemperatureValue { get; init; }
        public float? HumidityValue { get; init; }
    }
}
