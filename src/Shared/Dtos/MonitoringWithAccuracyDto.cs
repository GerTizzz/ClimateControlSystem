namespace Shared.Dtos
{
    public sealed class MonitoringWithAccuracyDto : BaseMonitoringDto
    {
        public AccuracyDto? Accuracy { get; set; }
    }
}
