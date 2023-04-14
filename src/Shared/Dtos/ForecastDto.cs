namespace Shared.Dtos;

public record ForecastDto
{
    public DateTimeOffset? Time { get; init; }
    public ErrorDto? Error { get; init; }
    public FactDto? Fact { get; init; }
    public LabelDto? Label { get; init; }
    public WarningDto? Warning { get; init; }
    public FeaturesDto? Feature { get; init; }
}