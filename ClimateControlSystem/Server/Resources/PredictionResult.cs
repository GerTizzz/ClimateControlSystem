namespace ClimateControlSystem.Server.Resources
{
    public record PredictionResult
    {
        public float PredictedTemperature { get; init; }
        public float PredictedHumidity { get; init; }
    }
}
