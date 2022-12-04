using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Hubs;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tensorflow;

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

            await _mediator.Send(new AddPredictionCommand()
            {
                Prediction = prediction, 
                Accuracy = accuracy, 
                Monitoring = monitoring,
                ClimateEventType = climateEvent
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

        private Task<ClimateEventType> GetClimateEventData(PredictionResult prediction)
        {
            ClimateEventType eventType = ClimateEventType.Normal;

            if (prediction.PredictedTemperature > _configManager.TemperatureLimit &&
                prediction.PredictedHumidity > _configManager.HumidityLimit)
            {
                eventType = ClimateEventType.Critical;
            }
            else if (prediction.PredictedTemperature > _configManager.TemperatureLimit)
            {
                eventType = ClimateEventType.PredictedTemperatureWarning;
            }
            else if (prediction.PredictedHumidity > _configManager.HumidityLimit)
            {
                eventType = ClimateEventType.PredictedHumidityWarning;
            }

            return Task.FromResult(eventType);
        }
    }
}
