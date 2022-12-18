using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Hubs;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.Enums;
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

        public async Task<PredictionResult> Predict(MonitoringData climateData)
        {
            PredictionResult prediction = await _predictionEngine.Predict(climateData);

            await CreateClimateRecord(climateData, prediction);

            Prediction predictionToSendToClient = new Prediction()
            {
                MeasurementTime = climateData.MeasurementTime,
                PredictedFutureTemperature = prediction.PredictedTemperature,
                PredictedFutureHumidity = prediction.PredictedHumidity,
                CurrentRealTemperature = climateData.CurrentRealTemperature,
                CurrentRealHumidity = climateData.CurrentRealHumidity
            };

            _ = SendNewMonitoringDataToWebClients(predictionToSendToClient);

            return prediction;
        }

        private async Task CreateClimateRecord(MonitoringData monitoring, PredictionResult prediction)
        {
            var accuracy = await CalculateLastPredictionAccuracy(monitoring);

            var climateEvent = await GetClimateEventData(prediction);

            await _mediator.Send(new AddClimateCommand()
            {
                Prediction = prediction,
                Monitoring = monitoring,
                ClimateEventType = climateEvent,
                Config = _configManager.Config
            });

            _ = _mediator.Send(new AddAccuracyCommand()
            {
                Accuracy = accuracy
            });
        }

        private async Task<AccuracyData> CalculateLastPredictionAccuracy(MonitoringData monitoringData)
        {
            var actualTemperature = monitoringData.CurrentRealTemperature;
            var actualHumidity = monitoringData.CurrentRealHumidity;

            PredictionResult lastRecord = await _mediator.Send(new GetLastPredictionQuery());

            var predictedTemperature = lastRecord.PredictedTemperature;
            var predictedHumidity = lastRecord.PredictedHumidity;

            AccuracyData accuracy = new AccuracyData()
            {
                PredictedTemperatureAccuracy = 100f - Math.Abs(predictedTemperature - actualTemperature) * 100 / actualTemperature,
                PredictedHumidityAccuracy = 100f - Math.Abs(predictedHumidity - actualHumidity) * 100 / actualHumidity
            };

            return accuracy;
        }

        private async Task SendNewMonitoringDataToWebClients(Prediction prediction)
        {
            await _monitoringHub.Clients.All.SendAsync("GetMonitoringData", prediction);
        }

        private Task<List<ClimateEventType>> GetClimateEventData(PredictionResult prediction)
        {
            List<ClimateEventType> eventTypes = new List<ClimateEventType>();

            if (prediction.PredictedTemperature >= _configManager.UpperTemperatureCriticalLimit ||
                prediction.PredictedTemperature <= _configManager.LowerTemperatureCriticalLimit)
            {
                eventTypes.Add(ClimateEventType.PredictedTemperatureCritical);
            }
            else if (prediction.PredictedTemperature < _configManager.UpperTemperatureCriticalLimit &&
                prediction.PredictedTemperature >= _configManager.UpperTemperatureWarningLimit ||
                prediction.PredictedTemperature > _configManager.LowerTemperatureCriticalLimit &&
                prediction.PredictedTemperature <= _configManager.LowerTemperatureWarningLimit)
            {
                eventTypes.Add(ClimateEventType.PredictedTemperatureWarning);
            }

            if (prediction.PredictedHumidity >= _configManager.UpperHumidityCriticalLimit ||
                prediction.PredictedHumidity <= _configManager.LowerHumidityCriticalLimit)
            {
                eventTypes.Add(ClimateEventType.PredictedHumidityCritical);
            }
            else if (prediction.PredictedHumidity < _configManager.UpperHumidityCriticalLimit &&
                prediction.PredictedHumidity >= _configManager.UpperHumidityWarningLimit ||
                prediction.PredictedHumidity > _configManager.LowerHumidityCriticalLimit &&
                prediction.PredictedHumidity <= _configManager.LowerHumidityWarningLimit)
            {
                eventTypes.Add(ClimateEventType.PredictedHumidityWarning);
            }

            if (eventTypes.Count == 0)
            {
                eventTypes.Add(ClimateEventType.Normal);
            }

            return Task.FromResult(eventTypes);
        }
    }
}
