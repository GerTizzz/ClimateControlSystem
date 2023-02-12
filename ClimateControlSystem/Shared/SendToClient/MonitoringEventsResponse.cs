namespace ClimateControlSystem.Shared.SendToClient
{
    public record class MonitoringEventsResponse
    {
        public DateTimeOffset? Time { get; set; }
        public TemperatureEventResponse? TemperatureEvent { get; set; }
        public HumidityEventResponse? HumidityEvent { get; set; }
    }
}
