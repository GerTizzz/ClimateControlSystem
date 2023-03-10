namespace Domain.Entities
{
    public sealed class FeaturesData
    {
        public float ClusterLoad { get; set; }
        public float CpuUsage { get; set; }
        public float ClusterTemperature { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float AirHumidityOutside { get; set; }
        public float AirDryTemperatureOutside { get; set; }
        public float AirWetTemperatureOutside { get; set; }
        public float WindSpeed { get; set; }
        public float WindDirection { get; set; }
        public float WindEnthalpy { get; set; }
        public float CoolingValue { get; set; }

        public FeaturesData Clone()
        {
            var clone = new FeaturesData
            {
                ClusterLoad = ClusterLoad,
                CpuUsage = CpuUsage,
                ClusterTemperature = ClusterTemperature,
                Temperature = Temperature,
                Humidity = Humidity,
                AirHumidityOutside = AirHumidityOutside,
                AirDryTemperatureOutside = AirDryTemperatureOutside,
                AirWetTemperatureOutside = AirWetTemperatureOutside,
                WindSpeed = WindSpeed,
                WindDirection = WindDirection,
                WindEnthalpy = WindEnthalpy,
                CoolingValue = CoolingValue
            };

            return clone;
        }
    }
}
