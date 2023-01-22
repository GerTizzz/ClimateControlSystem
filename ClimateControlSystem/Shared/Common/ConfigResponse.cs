﻿namespace ClimateControlSystem.Shared.Common
{
    public sealed class ConfigResponse
    {
        public float UpperTemperatureWarningLimit { get; set; }
        public float LowerTemperatureWarningLimit { get; set; }

        public float UpperHumidityWarningLimit { get; set; }
        public float LowerHumidityWarningLimit { get; set; }
    }
}