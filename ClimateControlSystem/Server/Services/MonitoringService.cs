using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.Domain;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using ClimateControlSystem.Server.Services.MediatR.Commands.MicroclimateRepository;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository;
using ClimateControlSystem.Server.Services.MediatR.Queries.PredictionEngine;
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

        public async Task<Prediction> Predict(FeaturesData featuresData)
        {
            Prediction prediction = await GetPrediction(featuresData);

            await ProcessMonitoringData(prediction, _configManager.Config);

            return prediction;
        }

        private async Task ProcessMonitoringData(Prediction prediction, Config config)
        {
            var actualData = GetActualDataFromFeaturesData(prediction.Features);

            var accuracy = await TryGetPredictionAccuracy(actualData);

            var microclimatesEvent = await TryGetMicroclimatesEvents(prediction, config);

            Monitoring monitoring = new MonitoringBuilder()
                .AddActualData(actualData)
                .AddAccuracy(accuracy)
                .AddTracedTime(DateTimeOffset.Now)
                .AddMicroclimatesEvent(microclimatesEvent)
                .AddPrediction(prediction)
                .Build();

            await SaveMonitoring(monitoring);

            await SendMonitoringToClients(monitoring);
        }

        /// <summary>
        /// Актуальная дата должна приходить снаружи! Это временная заглушка, т.к. по данной методике прогнозирования используются
        /// данные прогнозируемых величин. Из-за этого можно схитрить.
        /// </summary>
        private ActualData GetActualDataFromFeaturesData(FeaturesData features)
        {
            return new ActualData()
            {
                Temperature = features.MeasuredTemperature,
                Humidity = features.MeasuredHumidity
            };
        }

        private async Task<Accuracy?> TryGetPredictionAccuracy(ActualData actualData)
        {
            var prediction = await TryGetLastPrediction();

            if (actualData is null || prediction is null)
            {
                return null;
            }

            Accuracy accuracy = new Accuracy()
            {
                PredictedTemperatureAccuracy = 100f - Math.Abs(prediction.Temperature - actualData.Temperature) * 100 / actualData.Temperature,
                PredictedHumidityAccuracy = 100f - Math.Abs(prediction.Humidity - actualData.Humidity) * 100 / actualData.Humidity
            };

            return accuracy;
        }

        private Task<MicroclimatesEvents?> TryGetMicroclimatesEvents(Prediction prediction, Config config)
        {
            MicroclimateEventBuilder microclimateEventBuilder = new MicroclimateEventBuilder();

            if (prediction.Temperature >= config.UpperTemperatureWarningLimit)
            {
                microclimateEventBuilder.AddTemperatureEvent(prediction.Temperature - config.UpperTemperatureWarningLimit);
            }
            else if (prediction.Temperature <= config.LowerTemperatureWarningLimit)
            {
                microclimateEventBuilder.AddTemperatureEvent(prediction.Temperature - config.LowerTemperatureWarningLimit);
            }

            if (prediction.Humidity >= config.UpperHumidityWarningLimit)
            {
                microclimateEventBuilder.AddHumidityEvent(prediction.Humidity - config.UpperHumidityWarningLimit);
            }
            else if (prediction.Humidity <= config.LowerHumidityWarningLimit)
            {
                microclimateEventBuilder.AddHumidityEvent(prediction.Humidity - config.LowerHumidityWarningLimit);
            }

            return Task.FromResult(microclimateEventBuilder.Build());
        }

        private async Task<Prediction> GetPrediction(FeaturesData featuresData)
        {
            return await _mediator.Send(new GetPredictionQuery(featuresData));
        }

        private async Task<Prediction?> TryGetLastPrediction()
        {
            return await _mediator.Send(new TryGetLastPredictionQuery());
        }

        private async Task SendMonitoringToClients(Monitoring monitoring)
        {
            await _mediator.Send(new SendMonitoringCommand(monitoring));
        }

        private async Task SaveMonitoring(Monitoring monitoring)
        {
            await _mediator.Send(new SaveMonitoringCommand(monitoring));
        }        
    }
}
