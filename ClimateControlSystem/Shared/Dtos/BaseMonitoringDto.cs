namespace ClimateControl.Shared.Dtos
{
    public class BaseMonitoringDto
    {
        public DateTimeOffset? TracedTime { get; set; }
        public PredictionDto? Prediction { get; set; }
        public ActualDataDto? ActualData { get; set; }

        public BaseMonitoringDto CloneFull()
        {
            return new BaseMonitoringDto()
            {
                TracedTime = TracedTime,
                Prediction = Prediction?.Clone(),
                ActualData = ActualData?.Clone()
            };
        }

        public BaseMonitoringDto CloneWithPrediction()
        {
            return new BaseMonitoringDto()
            {
                Prediction = Prediction?.Clone()
            };
        }

        public BaseMonitoringDto CloneWithMeasuredAndTime()
        {
            return new BaseMonitoringDto()
            {
                TracedTime = TracedTime,
                ActualData = ActualData?.Clone()
            };
        }
    }
}
