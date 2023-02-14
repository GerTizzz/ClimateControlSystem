using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Infrastructure
{
    public sealed class MonitoringBuilder
    {
        private readonly Monitoring _monitoringData;

        public MonitoringBuilder()
        {
            _monitoringData = new Monitoring();
        }

        public MonitoringBuilder AddMeasurementTime(DateTimeOffset? time)
        {
            _monitoringData.MeasurementTime = time;

            return this;
        }

        public MonitoringBuilder AddPredictionData(Prediction prediction)
        {
            _monitoringData.Prediction = prediction;

            return this;
        }

        public MonitoringBuilder AddSensorsData(SensorsData sensors)
        {
            _monitoringData.MeasuredData = new MeasuredData()
            {
                MeasuredTemperature = sensors.MeasuredTemperature,
                MeasuredHumidity = sensors.MeasuredHumidity,
            };

            _monitoringData.MeasurementTime = sensors.MeasurementTime;

            return this;
        }

        public MonitoringBuilder AddAccuracy(Accuracy? accuracy)
        {
            _monitoringData.Accuracy = accuracy;

            return this;
        }

        public MonitoringBuilder AddMicroclimateEvent(MicroclimateEvent? temperatureEvent)
        {
            _monitoringData.MicroclimateEvent = temperatureEvent;

            return this;
        }

        public Monitoring Build()
        {
            return _monitoringData;
        }
    }
}
