using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Infrastructure
{
    public sealed class MonitoringDataBuilder
    {
        private readonly MonitoringData _monitoringData = new MonitoringData();

        public MonitoringDataBuilder AddPredictionData(PredictionResult prediction)
        {
            _monitoringData.HumidityPredictionForFuture = prediction.PredictedHumidity;
            _monitoringData.TemperaturePredictionForFuture = prediction.PredictedTemperature;

            return this;
        }

        public MonitoringDataBuilder AddSensorsData(SensorsData sensors)
        {
            _monitoringData.MeasuredHumidity = sensors.CurrentRealHumidity;
            _monitoringData.MeasuredTemperature = sensors.CurrentRealTemperature;
            _monitoringData.MeasurementTime = sensors.MeasurementTime;

            return this;
        }

        public MonitoringDataBuilder AddAccuracyData(PredictionAccuracy? accuracy)
        {
            if (accuracy is null)
            {
                _monitoringData.PreviousHumidityPredicitionAccuracy = null;
                _monitoringData.PredviousTemperaturePredictionAccuracy = null;
            }
            else
            {
                _monitoringData.PreviousHumidityPredicitionAccuracy = accuracy.PredictedHumidityAccuracy;
                _monitoringData.PredviousTemperaturePredictionAccuracy = accuracy.PredictedTemperatureAccuracy;
            }

            return this;
        }

        public MonitoringDataBuilder AddTemperatureEvent(TemperatureEvent? temperatureEvent)
        {
            _monitoringData.TemperaturePredictionEvent = temperatureEvent;

            return this;
        }

        public MonitoringDataBuilder AddHumidityEvent(HumidityEvent? humidityEvent)
        {
            _monitoringData.HumidityPredictionEvent = humidityEvent;

            return this;
        }

        public MonitoringData Build()
        {
            return _monitoringData;
        }
    }
}
