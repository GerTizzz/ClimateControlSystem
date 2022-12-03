﻿namespace ClimateControlSystem.Shared
{
    public sealed class MonitoringData
    {
        public DateTimeOffset MeasurementTime { get; set; }
        public float ClusterLoad { get; set; }
        public float CpuUsage { get; set; }
        public float ClusterTemperature { get; set; }
        public float CurrentRealTemperature { get; set; }
        public float CurrentRealHumidity { get; set; }
        public float AirHumidityOutside { get; set; }
        public float AirDryTemperatureOutside { get; set; }
        public float AirWetTemperatureOutside { get; set; }
        public float WindSpeed { get; set; }
        public float WindDirection { get; set; }
        public float WindEnthalpy { get; set; }
        public float MeanCoolingValue { get; set; }
    }
}
