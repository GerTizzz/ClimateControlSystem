﻿namespace ClimateControlSystem.Shared.SendToClient
{
    public record class MonitoringResponse
    {
        public DateTimeOffset MeasurementTime { get; set; }
        public float CurrentRealTemperature { get; set; }
        public float CurrentRealHumidity { get; set; }
        public float PredictedFutureTemperature { get; set; }
        public float PredictedFutureHumidity { get; set; }
        public float? PredictedTemperatureAccuracy { get; set; }
        public float? PredictedHumidityAccuracy { get; set; }
        public TemperatureEventResponse? TemperatureEvent { get; set; }
        public HumidityEventResponse? HumidityEvent { get; set; }
    }
}
