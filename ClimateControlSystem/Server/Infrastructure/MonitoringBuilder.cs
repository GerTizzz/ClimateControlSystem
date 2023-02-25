using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.Domain;

namespace ClimateControlSystem.Server.Infrastructure
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

        public MonitoringBuilder AddActualData(ActualData actualData)
        {
            _monitoring.ActualData = actualData;

            return this;
        }

        public MonitoringBuilder AddAccuracy(Accuracy? accuracy)
        {
            _monitoring.Accuracy = accuracy;

            return this;
        }

        public MonitoringBuilder AddMicroclimatesEvents(MicroclimatesEvents? microclimatesEvents)
        {
            _monitoring.MicroclimatesEvents = microclimatesEvents;

            return this;
        }

        public Monitoring Build()
        {
            return _monitoring;
        }
    }
}
