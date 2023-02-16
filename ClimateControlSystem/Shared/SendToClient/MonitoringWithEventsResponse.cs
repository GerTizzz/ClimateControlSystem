namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed class MonitoringWithEventsResponse : BaseMonitoringResponse
    {
        public MicroclimateEventResponse? MicroclimatesEvent { get; init; }
    }
}
