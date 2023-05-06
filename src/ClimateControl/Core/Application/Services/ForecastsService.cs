using Application.Helpers;
using Application.MediatR.ForecastsRepository;
using Application.MediatR.PredictionEngine;
using Application.MediatR.SignalR;
using Domain.Services;
using MediatR;

namespace Application.Services;

public class ForecastsService : IForecastsService
{
    private readonly IMediator _mediator;
    private readonly IConfigsManager _configManager;

    public ForecastsService(IMediator mediator,
        IConfigsManager configManager)
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
        var fact = GetActualDataFromFeaturesData(feature);

        var error = await TryGetError(fact);

        var microclimatesEvent = await TryGetWarning(label, config);

        var monitoring = new ForecastsBuilder()
            .AddActualData(fact)
            .AddError(error)
            .AddTracedTime(DateTimeOffset.Now)
            .AddWarning(microclimatesEvent)
            .AddPrediction(label)
            .AddFeature(feature)
            .Build();

        await SaveMonitoring(monitoring);

        await SendMonitoringToClients(monitoring);
    }

    /// <summary>
    /// Актуальная дата должна приходить снаружи! Это временная заглушка, т.к. по данной методике прогнозирования используются
    /// данные прогнозируемых величин.
    /// </summary>
    private static Fact GetActualDataFromFeaturesData(Feature features)
    {
        return new Fact(Guid.NewGuid())
        {
            Temperature = features.Temperature,
            Humidity = features.Humidity
        };
    }

    private async Task<Error?> TryGetError(Fact? actualData)
    {
        var prediction = await TryGetLastPrediction();

        if (actualData is null || prediction is null)
        {
            return null;
        }

        var temperature = Math.Abs(100f - prediction.Temperature * 100f / actualData.Temperature);
        var humidity = Math.Abs(100f - prediction.Humidity * 100f / actualData.Humidity);

        var error = new Error(Guid.NewGuid())
        {
            Temperature = temperature,
            Humidity = humidity
        };

        return error;
    }

    private static Task<Warning?> TryGetWarning(Label label, Config config)
    {
        var microclimateEventBuilder = new WarningsBuilder();

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