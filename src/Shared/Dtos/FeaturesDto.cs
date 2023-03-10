namespace Shared.Dtos
{
    public record FeaturesDto
    {
        public float ClusterLoad { get; init; }
        public float CpuUsage { get; init; }
        public float ClusterTemperature { get; init; }
        public float Temperature { get; init; }
        public float Humidity { get; init; }
        public float AirHumidityOutside { get; init; }
        public float AirDryTemperatureOutside { get; init; }
        public float AirWetTemperatureOutside { get; init; }
        public float WindSpeed { get; init; }
        public float WindDirection { get; init; }
        public float WindEnthalpy { get; init; }
        public float CoolingValue { get; init; }
    }
}
