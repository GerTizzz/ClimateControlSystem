using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Config : Entity
    {
        public float UpperTemperatureWarningLimit { get; set; }
        public float LowerTemperatureWarningLimit { get; set; }

        public float UpperHumidityWarningLimit { get; set; }
        public float LowerHumidityWarningLimit { get; set; }

        public int PredictionTimeIntervalSeconds { get; set; }

        public Config(Guid id,
            float upperTemperatureWarningLimit,
            float lowerTemperatureWarningLimit,
            float upperHumidityWarningLimit,
            float lowerHumidityWarningLimit,
            int predictionTimeIntervalSeconds) : base(id)
        {
            UpperTemperatureWarningLimit = upperTemperatureWarningLimit;
            LowerTemperatureWarningLimit = lowerTemperatureWarningLimit;
            UpperHumidityWarningLimit = upperHumidityWarningLimit;
            LowerHumidityWarningLimit = lowerHumidityWarningLimit;
            PredictionTimeIntervalSeconds = predictionTimeIntervalSeconds;
        }
    }
}
