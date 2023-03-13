namespace Shared.Dtos
{
    public record class ForecastDto
    {
        public DateTimeOffset? Time { get; init; }
        public FeaturesDto? Feature { get; init; }
        public LabelDto? Label { get; init; }
        public ErrorDto? Error { get; init; }
        public FactDto? Fact { get; init; }
        public WarningDto? Warning { get; init; }
    }
}
