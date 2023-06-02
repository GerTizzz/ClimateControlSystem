namespace Shared.Dtos;

public record ForecastDto
{
    public DateTimeOffset Time { get; init; }
    public List<PredictionDto> Predictions { get; init; }
    public FeaturesDto Feature { get; init; }
}