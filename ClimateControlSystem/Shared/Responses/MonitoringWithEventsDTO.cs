namespace ClimateControlSystem.Shared.Responses
{
    public sealed class MonitoringWithEventsDto : BaseMonitoringDto
    {
        public MicroclimatesEventsDto? MicroclimatesEvents { get; init; }
    }
}
