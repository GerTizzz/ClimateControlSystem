namespace ClimateControlSystem.Shared
{
    public sealed class Prediction
    {
        public DateTimeOffset MeasurementTime { get; init; }
        public float PredictedTemperature { get; init; }
        public float PredictedHumidity { get; init; }
        public float? RealTemperature { get; init; }
        public float? RealHumidity { get; init; }
    }
}
