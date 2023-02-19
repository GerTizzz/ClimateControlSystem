namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed class MonitoringWithAccuraciesDTO : BaseMonitoringDTO
    {
        public AccuracyDTO? Accuracy { get; set; }
    }
}
