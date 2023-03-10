namespace Shared.Dtos
{
    public sealed class MonitoringWithEventsDto : BaseMonitoringDto
    {
        public MicroclimatesEventsDto? MicroclimatesEvents { get; init; }
    }
}
