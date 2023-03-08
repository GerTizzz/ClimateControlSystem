namespace ClimateControl.Shared.Dtos
{
    public record class ForecastingDto
    {
        public DateTimeOffset? TracedTime { get; init; }
        public FeaturesDto? Features { get; init; }
        public PredictionDto? Prediction { get; init; }
        public AccuracyDto? Accuracy { get; init; }
        public ActualDataDto? ActualData { get; init; }
    }
}
