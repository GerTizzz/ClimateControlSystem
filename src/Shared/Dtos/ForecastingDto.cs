namespace Shared.Dtos
{
    public record class ForecastingDto
    {
        public DateTimeOffset? Time { get; init; }
        public FeaturesDto? Feature { get; init; }
        public PredictionDto? Label { get; init; }
        public AccuracyDto? Error { get; init; }
        public ActualDataDto? Fact { get; init; }
    }
}
