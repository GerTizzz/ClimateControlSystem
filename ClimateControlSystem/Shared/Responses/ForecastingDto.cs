﻿namespace ClimateControlSystem.Shared.Responses
{
    public record class ForecastingDto
    {
        public DateTimeOffset? TracedTime { get; init; }
        public FeaturesDto? Features { get; init; }
        public PredictionDto? Prediction { get; init; }
        public AccuracyDto? Accuracy { get; init; }
    }
}
