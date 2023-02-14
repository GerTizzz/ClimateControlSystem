namespace ClimateControlSystem.Server.Resources.Common
{
    public sealed class SensorsData
    {
        public DateTimeOffset MeasurementTime { get; set; }
        public float ClusterLoad { get; set; }
        public float CpuUsage { get; set; }
        public float ClusterTemperature { get; set; }
        public float MeasuredTemperature { get; set; }
        public float MeasuredHumidity { get; set; }
        public float AirHumidityOutside { get; set; }
        public float AirDryTemperatureOutside { get; set; }
        public float AirWetTemperatureOutside { get; set; }
        public float WindSpeed { get; set; }
        public float WindDirection { get; set; }
        public float WindEnthalpy { get; set; }
        public float MeanCoolingValue { get; set; }
    }
}
