namespace ClimateControlSystem.Shared.Responses
{
    public sealed class MonitoringWithEventsDTO : BaseMonitoringDTO
    {
        public MicroclimatesEventsDTO? MicroclimatesEvents { get; init; }
    }
}
