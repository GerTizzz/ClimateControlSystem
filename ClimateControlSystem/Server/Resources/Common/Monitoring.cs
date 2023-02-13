namespace ClimateControlSystem.Server.Resources.Common
{
    public class Monitoring
    {
        public DateTimeOffset? MeasurementTime { get; set; }
        public MeasuredData MeasuredData { get; set; }
        public Prediction Prediction { get; set; }
        public MicroclimateEvent? MicroclimateEvent { get; set; }
        public Accuracy? Accuracy { get; set; }
    }
}
