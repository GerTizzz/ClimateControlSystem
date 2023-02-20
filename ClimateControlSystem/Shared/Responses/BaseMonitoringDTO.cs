namespace ClimateControlSystem.Shared.Responses
{
    public class BaseMonitoringDTO
    {
        public DateTimeOffset? TracedTime { get; set; }
        public PredictionsDTO? Prediction { get; set; }
        public ActualDataDTO? ActualData { get; set; }

        public BaseMonitoringDTO CloneFull()
        {
            return new BaseMonitoringDTO()
            {
                TracedTime = TracedTime,
                Prediction = Prediction?.Clone(),
                ActualData = ActualData?.Clone()
            };
        }

        public BaseMonitoringDTO CloneWithPrediction()
        {
            return new BaseMonitoringDTO()
            {
                Prediction = Prediction?.Clone()
            };
        }

        public BaseMonitoringDTO CloneWithMeasuredAndTime()
        {
            return new BaseMonitoringDTO()
            {
                TracedTime = TracedTime,
                ActualData = ActualData?.Clone()
            };
        }
    }
}
