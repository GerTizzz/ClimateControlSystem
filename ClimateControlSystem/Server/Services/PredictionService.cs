using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Hubs;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Shared;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ClimateControlSystem.Server.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly IPredictionEngineService _predictionEngine;
        private readonly IMediator _mediator;
        private readonly IHubContext<MonitoringHub> _monitoringHub;
        private readonly IConfigManager _configManager;

        public PredictionService(IPredictionEngineService predictionEngine,
                                 IMediator mediator,
                                 IHubContext<MonitoringHub> monitoringHub,
                                 IConfigManager configManager)
        {
            _mediator = mediator;
            _predictionEngine = predictionEngine;
            _monitoringHub = monitoringHub;
            _configManager = configManager;
        }

        public async Task<PredictionResult> Predict(SensorsData climateData)
        {
            PredictionResult prediction = await _predictionEngine.Predict(climateData);

            await UpdatePreviousMicroclimateRecord(climateData);

            await CreateNewMicroclimateRecord(prediction);

            MonitoringResponse monitoring = new MonitoringResponse()
            {
                MeasurementTime = climateData.MeasurementTime,
                PredictedFutureTemperature = prediction.PredictedTemperature,
                PredictedFutureHumidity = prediction.PredictedHumidity,
                CurrentRealTemperature = climateData.CurrentRealTemperature,
                CurrentRealHumidity = climateData.CurrentRealHumidity
            };

            _ = SendNewMonitoringDataToWebClients(monitoring);

            return prediction;
        }

        private async Task UpdatePreviousMicroclimateRecord(SensorsData sensorData)
        {
            var accuracy = await CalculateLastPredictionAccuracy(sensorData);

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

        private async Task CreateNewMicroclimateRecord(PredictionResult prediction)
        {
            var temperatureEvent = await GetTemperatureEvent(prediction, _configManager.Config);

            var humidityEvent = await GetHumidityEvent(prediction, _configManager.Config);

            await _mediator.Send(new AddPredictionCommand()
            {
                Predicition = prediction,
                TemperatureEvent = temperatureEvent,
                HumidityEvent = humidityEvent
            });
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

        private async Task SendNewMonitoringDataToWebClients(MonitoringResponse monitoring)
        {
            await _monitoringHub.Clients.All.SendAsync("GetMonitoringData", monitoring);
        }

        private Task<TemperatureEvent> GetTemperatureEvent(PredictionResult prediction, Config config)
        {
            TemperatureEvent temperatureEvent = null;

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

        private Task<HumidityEvent> GetHumidityEvent(PredictionResult prediction, Config config)
        {
            HumidityEvent humidityEvent = null;

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
