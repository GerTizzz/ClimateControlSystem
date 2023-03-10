using Domain.Entities;

namespace WebApi.Infrastructure.GenerativePatterns
{
    public sealed class MonitoringBuilder
    {
        private readonly Monitoring _monitoring;

        public MonitoringBuilder()
        {
            _monitoring = new Monitoring();
        }

        public MonitoringBuilder AddTracedTime(DateTimeOffset? time)
        {
            _monitoring.TracedTime = time;

            return this;
        }

        public MonitoringBuilder AddPrediction(Prediction prediction)
        {
            _monitoring.Prediction = prediction;

            return this;
        }

        public MonitoringBuilder AddActualData(ActualData? actualData)
        {
            _monitoring.ActualData = actualData;

            return this;
        }

        public MonitoringBuilder AddAccuracy(Accuracy? accuracy)
        {
            _monitoring.Accuracy = accuracy;

            return this;
        }

        public MonitoringBuilder AddMicroclimatesEvent(MicroclimatesEvents? microclimatesEvents)
        {
            _monitoring.MicroclimatesEvent = microclimatesEvents;

            return this;
        }

        public Monitoring Build()
        {
            return _monitoring;
        }
    }
}
