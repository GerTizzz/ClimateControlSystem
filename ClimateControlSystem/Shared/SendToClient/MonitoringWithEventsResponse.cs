namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed record class MonitoringWithEventsResponse : BaseMonitoringResponse
    {
        public float? TemperatureValue { get; init; }
        public float? HumidityValue { get; init; }
    }
}
