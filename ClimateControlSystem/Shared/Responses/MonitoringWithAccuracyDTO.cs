namespace ClimateControlSystem.Shared.Responses
{
    public sealed class MonitoringWithAccuracyDTO : BaseMonitoringDTO
    {
        public AccuracyDTO? Accuracy { get; set; }
    }
}
