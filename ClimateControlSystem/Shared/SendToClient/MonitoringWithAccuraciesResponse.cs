namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed class MonitoringWithAccuraciesResponse : BaseMonitoringResponse
    {
        public float? PredictedTemperatureAccuracy { get; set; }
        public float? PredictedHumidityAccuracy { get; set; }
    }
}
