namespace ClimateControlSystem.Server.Resources.Common
{
    public record PredictionResult
    {
        public float PredictedTemperature { get; init; }
        public float PredictedHumidity { get; init; }
    }
}
