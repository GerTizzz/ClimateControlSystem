using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Infrastructure
{
    public sealed class MonitoringDataBuilder
    {
        private readonly MonitoringData _monitoringData = new MonitoringData();

        public MonitoringDataBuilder AddPredictionData(PredictionResult prediction)
        {
            _monitoringData.PredictedFutureHumidity = prediction.PredictedHumidity;
            _monitoringData.PredictedFutureTemperature = prediction.PredictedTemperature;

            return this;
        }

        public MonitoringDataBuilder AddSensorsData(SensorsData sensors)
        {
            _monitoringData.CurrentRealHumidity = sensors.CurrentRealHumidity;
            _monitoringData.CurrentRealTemperature = sensors.CurrentRealTemperature;
            _monitoringData.MeasurementTime = sensors.MeasurementTime;

            return this;
        }

        public MonitoringDataBuilder AddAccuracyData(PredictionAccuracy? accuracy)
        {
            if (accuracy is null)
            {
                _monitoringData.PredictedHumidityAccuracy = null;
                _monitoringData.PredictedTemperatureAccuracy = null;
            }
            else
            {
                _monitoringData.PredictedHumidityAccuracy = accuracy.PredictedHumidityAccuracy;
                _monitoringData.PredictedTemperatureAccuracy = accuracy.PredictedTemperatureAccuracy;
            }

            return this;
        }

        public MonitoringDataBuilder AddTemperatureEvent(TemperatureEvent? temperatureEvent)
        {
            _monitoringData.TemperatureEvent = temperatureEvent;

            return this;
        }

        public MonitoringDataBuilder AddHumidityEvent(HumidityEvent? humidityEvent)
        {
            _monitoringData.HumidityEvent = humidityEvent;

            return this;
        }

        public MonitoringData Build()
        {
            return _monitoringData;
        }
    }
}
