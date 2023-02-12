namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed record class MonitoringWithAccuraciesResponse : BaseMonitoringResponse
    {
        public float? PredictedTemperatureAccuracy { get; set; }
        public float? PredictedHumidityAccuracy { get; set; }
    }
}
