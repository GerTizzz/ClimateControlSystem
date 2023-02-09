using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Infrastructure
{
    public sealed class MonitoringWithEventsBuilder
    {
        private readonly MonitoringWithEvents _monitoringData = new MonitoringWithEvents();

        public MonitoringWithEventsBuilder AddPredictionData(PredictionResult prediction)
        {
            _monitoringData.HumidityPredictionForFuture = prediction.PredictedHumidity;
            _monitoringData.TemperaturePredictionForFuture = prediction.PredictedTemperature;

            return this;
        }

        public MonitoringWithEventsBuilder AddSensorsData(SensorsData sensors)
        {
            _monitoringData.MeasuredHumidity = sensors.CurrentRealHumidity;
            _monitoringData.MeasuredTemperature = sensors.CurrentRealTemperature;
            _monitoringData.MeasurementTime = sensors.MeasurementTime;

            return this;
        }

        public MonitoringWithEventsBuilder AddTemperatureEvent(TemperatureEvent? temperatureEvent)
        {
            _monitoringData.TemperaturePredictionEvent = temperatureEvent;

            return this;
        }

        public MonitoringWithEventsBuilder AddHumidityEvent(HumidityEvent? humidityEvent)
        {
            _monitoringData.HumidityPredictionEvent = humidityEvent;

            return this;
        }

        public MonitoringWithEvents Build()
        {
            return _monitoringData;
        }
    }
}
