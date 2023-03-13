namespace Shared.Dtos
{
    public record WarningDto
    {
        public DateTimeOffset? Time { get; set; }
        public float? TemperatureValue { get; init; }
        public float? HumidityValue { get; init; }
    }
}
