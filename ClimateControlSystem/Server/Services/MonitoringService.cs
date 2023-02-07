using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using MediatR;

namespace ClimateControlSystem.Server.Services
{
    public class MonitoringService : IMonitoringService
    {
        private readonly IPredictionEngineService _predictionEngine;
        private readonly IMediator _mediator;
        private readonly IConfigManager _configManager;

        public MonitoringService(IPredictionEngineService predictionEngine,
                                 IMediator mediator,
                                 IConfigManager configManager)
        {
            _mediator = mediator;
            _predictionEngine = predictionEngine;
            _configManager = configManager;
        }

        public async Task<PredictionResult> Predict(SensorsData sensorsData)
        {
            PredictionResult prediction = await _predictionEngine.Predict(sensorsData);

            _ = Task.Run(() => ProcessMonitoringData(sensorsData, prediction));

            return prediction;
        }

        private async Task ProcessMonitoringData(SensorsData sensorsData, PredictionResult prediction)
        {
            var accuracy = await CalculateLastPredictionAccuracy(sensorsData);

            var temperatureEvent = await GetTemperatureEvent(prediction, _configManager.Config);

            var humidityEvent = await GetHumidityEvent(prediction, _configManager.Config);

            await UpdatePreviousMonitoringInDb(sensorsData, accuracy);

            await WriteNewMonitroingToDb(prediction, temperatureEvent, humidityEvent);

            MonitoringData monitoring = new MonitoringDataBuilder()
                .AddSensorsData(sensorsData)
                .AddPredictionData(prediction)
                .AddAccuracyData(accuracy)
                .AddTemperatureEvent(temperatureEvent)
                .AddHumidityEvent(humidityEvent)
                .Build();

            _ = Task.Run(() => SendMonitoringToCustomers(monitoring));
        }

        private async Task SendMonitoringToCustomers(MonitoringData monitoring)
        {
            await _mediator.Send(new SendMonitoringCommand()
            {
                Monitoring = monitoring
            });
        }

        private async Task UpdatePreviousMonitoringInDb(SensorsData sensorData, PredictionAccuracy? accuracy)
        {
            await _mediator.Send(new AddSensorsDataCommand()
            {
                SensorData = sensorData
            });

            if (accuracy is not null)
            {
                await _mediator.Send(new AddAccuracyCommand()
                {
                    Accuracy = accuracy
                });
            }
        }

        private async Task<PredictionAccuracy?> CalculateLastPredictionAccuracy(SensorsData sensorData)
        {
            var actualTemperature = sensorData.CurrentRealTemperature;
            var actualHumidity = sensorData.CurrentRealHumidity;

            PredictionResult? lastRecord = await _mediator.Send(new GetLastPredictionQuery());

            if (lastRecord is null)
            {
                return null;
            }

            var predictedTemperature = lastRecord.PredictedTemperature;
            var predictedHumidity = lastRecord.PredictedHumidity;

            PredictionAccuracy accuracy = new PredictionAccuracy()
            {
                PredictedTemperatureAccuracy = 100f - Math.Abs(predictedTemperature - actualTemperature) * 100 / actualTemperature,
                PredictedHumidityAccuracy = 100f - Math.Abs(predictedHumidity - actualHumidity) * 100 / actualHumidity
            };

            return accuracy;
        }

        private async Task WriteNewMonitroingToDb(PredictionResult prediction, TemperatureEvent? temperatureEvent, HumidityEvent? humidityEvent)
        {
            await _mediator.Send(new AddPredictionCommand()
            {
                Predicition = prediction,
                TemperatureEvent = temperatureEvent,
                HumidityEvent = humidityEvent
            });
        }

        private Task<TemperatureEvent?> GetTemperatureEvent(PredictionResult prediction, Config config)
        {
            TemperatureEvent? temperatureEvent = null;

            if (prediction.PredictedTemperature >= config.UpperTemperatureWarningLimit)
            {
                temperatureEvent = new TemperatureEvent()
                {
                    Value = prediction.PredictedHumidity - config.UpperTemperatureWarningLimit
                };
            }
            else if (prediction.PredictedHumidity <= config.LowerTemperatureWarningLimit)
            {
                temperatureEvent = new TemperatureEvent()
                {
                    Value = prediction.PredictedHumidity - config.LowerTemperatureWarningLimit
                };
            }

            return Task.FromResult(temperatureEvent);
        }

        private Task<HumidityEvent?> GetHumidityEvent(PredictionResult prediction, Config config)
        {
            HumidityEvent? humidityEvent = null;

            if (prediction.PredictedHumidity >= config.UpperHumidityWarningLimit)
            {
                humidityEvent = new HumidityEvent()
                {
                    Value = prediction.PredictedHumidity - config.UpperHumidityWarningLimit
                };
            }
            else if (prediction.PredictedHumidity <= config.LowerHumidityWarningLimit)
            {
                humidityEvent = new HumidityEvent()
                {
                    Value = prediction.PredictedHumidity - config.LowerHumidityWarningLimit
                };
            }

            return Task.FromResult(humidityEvent);
        }
    }
}
