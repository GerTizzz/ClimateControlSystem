namespace ClimateControlSystem.Shared.SendToClient
{
    public readonly struct MicroclimateResponse
    {
        public DateTimeOffset MeasurementTime { get; init; }
        public float ClusterLoad { get; init; }
        public float CpuUsage { get; init; }
        public float ClusterTemperature { get; init; }
        public float CurrentRealTemperature { get; init; }
        public float CurrentRealHumidity { get; init; }
        public float AirHumidityOutside { get; init; }
        public float AirDryTemperatureOutside { get; init; }
        public float AirWetTemperatureOutside { get; init; }
        public float WindSpeed { get; init; }
        public float WindDirection { get; init; }
        public float WindEnthalpy { get; init; }
        public float MeanCoolingValue { get; init; }
        public float PredictedFutureTemperature { get; init; }
        public float PredictedFutureHumidity { get; init; }
        public float? PredictedTemperatureAccuracy { get; init; }
        public float? PredictedHumidityAccuracy { get; init; }
    }
}
