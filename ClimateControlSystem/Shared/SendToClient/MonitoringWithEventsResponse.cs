namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed record class MonitoringWithEventsResponse : BaseMonitoringResponse
    {
        public TemperatureEventResponse? TemperaturePredictionEvent { get; set; }
        public HumidityEventResponse? HumidityPredictionEvent { get; set; }
    }
}
