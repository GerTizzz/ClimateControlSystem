using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Infrastructure
{
    public sealed class MonitoringBuilder
    {
        private readonly Monitoring _monitoringData = new Monitoring();

        public MonitoringBuilder AddPredictionData(PredictionResult prediction)
        {
            _monitoringData.PredictedHumidity = prediction.PredictedHumidity;
            _monitoringData.PredictedTemperature = prediction.PredictedTemperature;

            return this;
        }

        public MonitoringBuilder AddSensorsData(SensorsData sensors)
        {
            _monitoringData.MeasuredHumidity = sensors.CurrentRealHumidity;
            _monitoringData.MeasuredTemperature = sensors.CurrentRealTemperature;
            _monitoringData.MeasurementTime = sensors.MeasurementTime;

            return this;
        }

        public MonitoringBuilder AddTemperatureEvent(TemperatureEvent? temperatureEvent)
        {
            _monitoringData.TemperaturePredictionEvent = temperatureEvent;

            return this;
        }

        public MonitoringBuilder AddHumidityEvent(HumidityEvent? humidityEvent)
        {
            _monitoringData.HumidityPredictionEvent = humidityEvent;

            return this;
        }

        public Monitoring Build()
        {
            return _monitoringData;
        }
    }
}
