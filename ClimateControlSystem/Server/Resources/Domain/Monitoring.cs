using ClimateControlSystem.Server.Resources.Domain;

namespace ClimateControlSystem.Server.Resources.Common
{
    public class Monitoring
    {
        public DateTimeOffset? TracedTime { get; set; }
        public ActualData ActualData { get; set; }
        public Prediction Prediction { get; set; }
        public MicroclimatesEvents? MicroclimatesEvent { get; set; }
        public Accuracy? Accuracy { get; set; }

        public Monitoring Clone()
        {
            var clone = new Monitoring()
            {
                TracedTime = TracedTime,
                ActualData = ActualData.Clone(),
                Prediction = Prediction.Clone(),
                MicroclimatesEvent = MicroclimatesEvent?.Clone(),
                Accuracy = Accuracy?.Clone(),
            };

            return clone;
        }
    }
}
