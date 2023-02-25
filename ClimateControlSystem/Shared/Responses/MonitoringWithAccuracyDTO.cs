namespace ClimateControlSystem.Shared.Responses
{
    public sealed class MonitoringWithAccuracyDto : BaseMonitoringDto
    {
        public AccuracyDto? Accuracy { get; set; }
    }
}
