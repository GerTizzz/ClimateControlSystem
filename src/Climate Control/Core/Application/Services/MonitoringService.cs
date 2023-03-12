using Application.CreationalPatterns;
using Application.MediatR.ForecastRepository;
using Application.MediatR.PredictionEngine;
using Application.MediatR.SignalR;
using Domain.Services;
using MediatR;

namespace Application.Services
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

        public async Task<Label> Predict(Feature feature)
        {
            var label = await GetPrediction(feature);

            await ProcessMonitoringData(label, feature, _configManager.Config);

            return label;
        }

        private async Task ProcessMonitoringData(Label label, Feature feature, Config config)
        {
            var actualData = GetActualDataFromFeaturesData(feature);

            var accuracy = await TryGetPredictionAccuracy(actualData);

            var microclimatesEvent = await TryGetMicroclimatesEvents(label, config);

            var monitoring = new ForecastBuilder()
                .AddActualData(actualData)
                .AddAccuracy(accuracy)
                .AddTracedTime(DateTimeOffset.Now)
                .AddMicroclimatesEvent(microclimatesEvent)
                .AddPrediction(label)
                .Build();

            await SaveMonitoring(monitoring);

            await SendMonitoringToClients(monitoring);
        }

        /// <summary>
        /// Актуальная дата должна приходить снаружи! Это временная заглушка, т.к. по данной методике прогнозирования используются
        /// данные прогнозируемых величин. Из-за этого можно схитрить.
        /// </summary>
        private static Fact GetActualDataFromFeaturesData(Feature features)
        {
            return new Fact
            {
                Temperature = features.Temperature,
                Humidity = features.Humidity
            };
        }

        private async Task<Accuracy?> TryGetPredictionAccuracy(Fact? actualData)
        {
            var prediction = await TryGetLastPrediction();

            if (actualData is null || prediction is null)
            {
                return null;
            }

            var temperature = 100f - Math.Abs(prediction.Temperature - actualData.Temperature) * 100f / actualData.Temperature;
            var humidity = 100f - Math.Abs(prediction.Humidity - actualData.Humidity) * 100f / actualData.Humidity;

            var accuracy = new Accuracy(Guid.NewGuid(), temperature, humidity);

            return accuracy;
        }

        private static Task<Warning?> TryGetMicroclimatesEvents(Label label, Config config)
        {
            var microclimateEventBuilder = new WarningBuilder();

            if (label.Temperature >= config.UpperTemperatureWarningLimit)
            {
                microclimateEventBuilder.AddTemperatureEvent(label.Temperature - config.UpperTemperatureWarningLimit);
            }
            else if (label.Temperature <= config.LowerTemperatureWarningLimit)
            {
                microclimateEventBuilder.AddTemperatureEvent(label.Temperature - config.LowerTemperatureWarningLimit);
            }

            if (label.Humidity >= config.UpperHumidityWarningLimit)
            {
                microclimateEventBuilder.AddHumidityEvent(label.Humidity - config.UpperHumidityWarningLimit);
            }
            else if (label.Humidity <= config.LowerHumidityWarningLimit)
            {
                microclimateEventBuilder.AddHumidityEvent(label.Humidity - config.LowerHumidityWarningLimit);
            }

            return Task.FromResult(microclimateEventBuilder.Build());
        }

        private async Task<Label> GetPrediction(Feature feature)
        {
            return await _mediator.Send(new GetPredictionQuery(feature));
        }

        private async Task<Label?> TryGetLastPrediction()
        {
            return await _mediator.Send(new TryGetLastPredictionQuery());
        }

        private async Task SendMonitoringToClients(Forecast forecast)
        {
            await _mediator.Send(new SendNewForecastToClientsQuery(forecast));
        }

        private async Task SaveMonitoring(Forecast forecast)
        {
            await _mediator.Send(new SaveForecastCommand(forecast));
        }
    }
}
