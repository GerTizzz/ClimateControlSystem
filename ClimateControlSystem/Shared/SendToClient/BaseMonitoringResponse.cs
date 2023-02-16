namespace ClimateControlSystem.Shared.SendToClient
{
    public class BaseMonitoringResponse
    {
        public DateTimeOffset? MeasurementTime { get; set; }
        public PredictionResponse? Prediction { get; set; }
        public ActualDataResponse? ActualData { get; set; }

        public BaseMonitoringResponse CloneFull()
        {
            return new BaseMonitoringResponse()
            { 
                MeasurementTime = MeasurementTime,
                Prediction = Prediction?.Clone(),
                ActualData = ActualData?.Clone()
            };
        }

        public BaseMonitoringResponse CloneWithPrediction()
        {
            return new BaseMonitoringResponse()
            {
                Prediction = Prediction?.Clone()
            };
        }

        public BaseMonitoringResponse CloneWithMeasuredAndTime()
        {
            return new BaseMonitoringResponse()
            {
                MeasurementTime = MeasurementTime,
                ActualData = ActualData?.Clone()
            };
        }
    }
}
