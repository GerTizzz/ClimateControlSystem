using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Hubs;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.Common;
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

        public async Task<PredictionResultData> Predict(SensorsData climateData)
        {
            PredictionResultData prediction = await _predictionEngine.Predict(climateData);

            await CreateClimateRecord(climateData, prediction);

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

        private async Task CreateClimateRecord(SensorsData sensorData, PredictionResultData prediction)
        {
            var accuracy = await CalculateLastPredictionAccuracy(sensorData);

            var temperatureEvent = await GetTemperatureEvent(prediction, _configManager.Config);

            var humidityEvent = await GetHumidityEvent(prediction, _configManager.Config);

            await _mediator.Send(new AddClimateCommand()
            {
                Prediction = prediction,
                SensorData = sensorData,
                TemperatureEvent = temperatureEvent,
                HumidityEvent = humidityEvent
            });

            _ = _mediator.Send(new AddAccuracyCommand()
            {
                Accuracy = accuracy
            });
        }

        private async Task<AccuracyData> CalculateLastPredictionAccuracy(SensorsData sensorData)
        {
            var actualTemperature = sensorData.CurrentRealTemperature;
            var actualHumidity = sensorData.CurrentRealHumidity;

            PredictionResultData lastRecord = await _mediator.Send(new GetLastPredictionQuery());

            var predictedTemperature = lastRecord.PredictedTemperature;
            var predictedHumidity = lastRecord.PredictedHumidity;

            AccuracyData accuracy = new AccuracyData()
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

        private Task<TemperatureEventData> GetTemperatureEvent(PredictionResultData prediction, Config config)
        {
            TemperatureEventData temperatureEvent = null;

            if (prediction.PredictedTemperature >= config.UpperTemperatureWarningLimit)
            {
                temperatureEvent = new TemperatureEventData()
                {
                    Value = prediction.PredictedHumidity - config.UpperTemperatureWarningLimit
                };
            }
            else if (prediction.PredictedHumidity <= config.LowerTemperatureWarningLimit)
            {
                temperatureEvent = new TemperatureEventData()
                {
                    Value = prediction.PredictedHumidity - config.LowerTemperatureWarningLimit
                };
            }

            return Task.FromResult(temperatureEvent);
        }

        private Task<HumidityEventData> GetHumidityEvent(PredictionResultData prediction, Config config)
        {
            HumidityEventData humidityEvent = null;

            if (prediction.PredictedHumidity >= config.UpperHumidityWarningLimit)
            {
                humidityEvent = new HumidityEventData()
                {
                    Value = prediction.PredictedHumidity - config.UpperHumidityWarningLimit
                };
            }
            else if (prediction.PredictedHumidity <= config.LowerHumidityWarningLimit)
            {
                humidityEvent = new HumidityEventData()
                {
                    Value = prediction.PredictedHumidity - config.LowerHumidityWarningLimit
                };
            }

            return Task.FromResult(humidityEvent);
        }
    }
}
