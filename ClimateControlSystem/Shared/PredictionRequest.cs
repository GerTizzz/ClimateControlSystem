namespace ClimateControlSystem.Shared
{
    public record PredictionRequest
    {
        public float ClusterLoad { get; init; }
        public float CpuUsage { get; init; }
        public float ClusterTemperature { get; init; }
        public float PreviousTemperature { get; init; }
        public float PreviousHumidity { get; init; }
        public float AirHumidityOutside { get; init; }
        public float AirDryTemperatureOutside { get; init; }
        public float AirWetTemperatureOutside { get; init; }
        public float WindSpeed { get; init; }
        public float WindDirection { get; init; }
        public float WindEnthalpy { get; init; }
        public float MeanCoolingValue { get; init; }
    }
}
