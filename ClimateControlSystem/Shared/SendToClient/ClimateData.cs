namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed class ClimateData
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
        public float PredictedFutureTemperature { get; init; }
        public float PredictedFutureHumidity { get; init; }
        public float? PredictedTemperatureAccuracy { get; init; }
        public float? PredictedHumidityAccuracy { get; init; }
    }
}
