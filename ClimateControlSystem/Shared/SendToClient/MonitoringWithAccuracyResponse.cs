namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed record class MonitoringWithAccuracyResponse : BaseMonitoringResponse
    {
        public float? PreviousTemperaturePredictionAccuracy { get; set; }
        public float? PreviousHumidityPredicitionAccuracy { get; set; }
    }
}
