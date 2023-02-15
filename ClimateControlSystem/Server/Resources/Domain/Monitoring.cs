namespace ClimateControlSystem.Server.Resources.Common
{
    public class Monitoring
    {
        public DateTimeOffset? MeasurementTime { get; set; }
        public MeasuredData MeasuredData { get; set; }
        public Prediction Prediction { get; set; }
        public MicroclimateEvent? MicroclimateEvent { get; set; }
        public Accuracy? Accuracy { get; set; }
        public SensorsData? SensorsData { get; set; }

        public Monitoring Clone()
        {
            var clone = new Monitoring()
            {
                MeasurementTime = MeasurementTime,
                MeasuredData = MeasuredData.Clone(),
                Prediction = Prediction.Clone(),
                MicroclimateEvent = MicroclimateEvent?.Clone(),
                Accuracy = Accuracy?.Clone(),
                SensorsData = SensorsData?.Clone()
            };

            return clone;
        }
    }
}
