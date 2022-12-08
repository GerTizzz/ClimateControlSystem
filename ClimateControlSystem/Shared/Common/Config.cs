namespace ClimateControlSystem.Shared.Common
{
    public sealed class Config
    {
        public float TemperatureLimit { get; set; }
        public float HumidityLimit { get; set; }
        
        public float UpperTemperatureWarningLimit { get; set; }
        public float LowerTemperatureWarningLimit { get; set; }
        public float UpperTemperatureCriticalLimit { get; set; }
        public float LowerTemperatureCriticalLimit { get; set; }

        public float UpperHumidityWarningLimit { get; set; }
        public float LowerHumidityWarningLimit { get; set; }
        public float UpperHumidityCriticalLimit { get; set; }
        public float LowerHumidityCriticalLimit { get; set; }
    }
}
