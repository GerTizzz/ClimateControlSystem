namespace Shared.Dtos
{
    public record WarningDto
    {
        public DateTimeOffset? Time { get; set; }
        public float? Temperature { get; init; }
        public float? Humidity { get; init; }
    }
}
