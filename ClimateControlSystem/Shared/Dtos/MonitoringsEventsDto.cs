namespace ClimateControl.Shared.Dtos
{
    public record class MonitoringsEventsDto
    {
        public DateTimeOffset? Time { get; set; }
        public float? TemperatureValue { get; init; }
        public float? HumidityValue { get; init; }
    }
}
