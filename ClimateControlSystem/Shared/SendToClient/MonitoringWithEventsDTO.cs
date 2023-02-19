namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed class MonitoringWithEventsDTO : BaseMonitoringDTO
    {
        public MicroclimateEventDTO? MicroclimatesEvent { get; init; }
    }
}
