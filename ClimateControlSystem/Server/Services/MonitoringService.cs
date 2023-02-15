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
        private readonly IMediator _mediator;
        private readonly IConfigManager _configManager;

        public MonitoringService(IMediator mediator,
                                 IConfigManager configManager)
        {
            _mediator = mediator;
            _configManager = configManager;
        }

        public async Task<Prediction> Predict(SensorsData sensorsData)
        {
            Prediction prediction = await GetPrediction(sensorsData);

            await ProcessMonitoringData(sensorsData, prediction);

            return prediction;
        }

        private async Task ProcessMonitoringData(SensorsData sensorsData, Prediction prediction)
        {
            var lastPrediction = await GetLastPrediction();

            var accuracy = CalculateLastPredictionAccuracy(sensorsData, lastPrediction);

            var microclimateEvent = await GetMicroclimateEvent(prediction, _configManager.Config);

            await SaveOrUpdateSensorsData(sensorsData);

            Monitoring monitoringToSave = new MonitoringBuilder()
                .AddAccuracy(accuracy)
                .AddMicroclimateEvent(microclimateEvent)
                .AddPredictionData(prediction)
                .Build();

            await SaveMonitoring(monitoringToSave);

            Monitoring monitoringToSend = new MonitoringBuilder()
                .AddMeasuredData(sensorsData)
                .AddAccuracy(accuracy)
                .AddMicroclimateEvent(microclimateEvent)
                .AddPredictionData(prediction)
                .Build();

            await SendMonitoringToClients(monitoringToSend);
        }

        private Accuracy? CalculateLastPredictionAccuracy(SensorsData sensorData, Prediction? prediction)
        {
            if (sensorData is null || prediction is null)
            {
                return null;
            }

            var actualTemperature = sensorData.MeasuredTemperature;
            var actualHumidity = sensorData.MeasuredHumidity;

            var predictedTemperature = prediction.PredictedTemperature;
            var predictedHumidity = prediction.PredictedHumidity;

            Accuracy accuracy = new Accuracy()
            {
                PredictedTemperatureAccuracy = 100f - Math.Abs(predictedTemperature - actualTemperature) * 100 / actualTemperature,
                PredictedHumidityAccuracy = 100f - Math.Abs(predictedHumidity - actualHumidity) * 100 / actualHumidity
            };

            return accuracy;
        }

        private Task<MicroclimateEvent?> GetMicroclimateEvent(Prediction prediction, Config config)
        {
            MicroclimateEventBuilder microclimateEventBuilder = new MicroclimateEventBuilder();

            if (prediction.PredictedTemperature >= config.UpperTemperatureWarningLimit)
            {
                microclimateEventBuilder.AddTemperatureEvent(prediction.PredictedHumidity - config.UpperTemperatureWarningLimit);
            }
            else if (prediction.PredictedHumidity <= config.LowerTemperatureWarningLimit)
            {
                microclimateEventBuilder.AddTemperatureEvent(prediction.PredictedHumidity - config.LowerTemperatureWarningLimit);
            }

            if (prediction.PredictedHumidity >= config.UpperHumidityWarningLimit)
            {
                microclimateEventBuilder.AddHumidityEvent(prediction.PredictedHumidity - config.UpperHumidityWarningLimit);
            }
            else if (prediction.PredictedHumidity <= config.LowerHumidityWarningLimit)
            {
                microclimateEventBuilder.AddHumidityEvent(prediction.PredictedHumidity - config.LowerHumidityWarningLimit);
            }

            return Task.FromResult(microclimateEventBuilder.Build());
        }

        private async Task<Prediction> GetPrediction(SensorsData sensorsData)
        {
            return await _mediator.Send(new GetPredictionQuery(sensorsData));
        }

        private async Task<Prediction?> GetLastPrediction()
        {
            return await _mediator.Send(new GetLastPredictionQuery());
        }

        private async Task SendMonitoringToClients(Monitoring monitoring)
        {
            await _mediator.Send(new SendMonitoringCommand(monitoring));
        }

        private async Task SaveOrUpdateSensorsData(SensorsData sensorsData)
        {
            await _mediator.Send(new SaveSensorsDataCommand(sensorsData));
        }

        private async Task SaveMonitoring(Monitoring monitoring)
        {
            await _mediator.Send(new SaveMonitoringCommand(monitoring));
        }        
    }
}
