namespace ClimateControlSystem.Server.Resources.Common
{
    public sealed class SensorsData
    {
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

        public SensorsData Clone()
        {
            var clone = new SensorsData()
            {
                ClusterLoad = ClusterLoad,
                CpuUsage = CpuUsage,
                ClusterTemperature = ClusterTemperature,
                MeasuredTemperature = MeasuredTemperature,
                MeasuredHumidity = MeasuredHumidity,
                AirHumidityOutside = AirHumidityOutside,
                AirDryTemperatureOutside = AirDryTemperatureOutside,
                AirWetTemperatureOutside = AirWetTemperatureOutside,
                WindSpeed = WindSpeed,
                WindDirection = WindDirection,
                WindEnthalpy = WindEnthalpy,
                MeanCoolingValue = MeanCoolingValue
            };

            return clone;
        }
    }
}
