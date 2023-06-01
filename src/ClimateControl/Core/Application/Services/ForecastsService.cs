using Application.Helpers;
using Application.MediatR.ForecastsRepository;
using Application.MediatR.PredictionEngine;
using Application.MediatR.SignalR;
using Application.Primitives;
using Domain.Services;
using MediatR;

namespace Application.Services;

public class ForecastsService : IForecastsService
{
    private readonly IMediator _mediator;
    private readonly IConfigsManager _configManager;
    private readonly IList<float> _featuresBuffer;

    public ForecastsService(IMediator mediator,
        IConfigsManager configManager)
    {
        _mediator = mediator;
        _configManager = configManager;
        _featuresBuffer = new List<float>();
    }

    public async Task<PredictedValue> Predict(Feature feature)
    {
        var predictedValue = await GetPrediction(feature);

        await ProcessMonitoringData(predictedValue, feature, _configManager.Config);

        return predictedValue;
    }

    private async Task ProcessMonitoringData(PredictedValue predictedValue, Feature feature, Config config)
    {
        _featuresBuffer.Add(feature.TemperatureInside);
        _featuresBuffer.Add(feature.TemperatureOutside);
        _featuresBuffer.Add(feature.CoolingPower);
        
        while (_featuresBuffer.Count == TensorPredictionRequest.InputSize)
        {
            _featuresBuffer.RemoveAt(0);
        }
        
        var actualValue = GetActualDataFromFeaturesData(feature);

        var error = await TryGetError(actualValue);

        var microclimatesEvent = await TryGetWarning(predictedValue, config);

        var monitoring = new ForecastsBuilder()
            .AddActualData(actualValue)
            .AddError(error)
            .AddTracedTime(DateTimeOffset.Now)
            .AddWarning(microclimatesEvent)
            .AddPrediction(predictedValue)
            .AddFeature(feature)
            .Build();

        await SaveMonitoring(monitoring);

        await SendMonitoringToClients(monitoring);
    }

    /// <summary>
    /// Актуальная дата должна приходить снаружи! Это временная заглушка, т.к. по данной методике прогнозирования используются
    /// данные прогнозируемых величин.
    /// </summary>
    private static ActualValue GetActualDataFromFeaturesData(Feature features)
    {
        return new ActualValue(Guid.NewGuid())
        {
            Temperature = features.TemperatureInside
        };
    }

    private async Task<Error?> TryGetError(ActualValue? actualData)
    {
        var prediction = await TryGetLastPrediction();

        if (actualData is null || prediction is null)
        {
            return null;
        }

        var temperature = Math.Abs(100f - prediction.Temperature * 100f / actualData.Temperature);

        var error = new Error(Guid.NewGuid())
        {
            Temperature = temperature
        };

        return error;
    }

    private static Task<Warning?> TryGetWarning(PredictedValue predictedValue, Config config)
    {
        var microclimateEventBuilder = new WarningsBuilder();

        if (predictedValue.Temperature >= config.UpperTemperatureWarningLimit)
        {
            microclimateEventBuilder.AddTemperatureEvent(predictedValue.Temperature - config.UpperTemperatureWarningLimit);
        }
        else if (predictedValue.Temperature <= config.LowerTemperatureWarningLimit)
        {
            microclimateEventBuilder.AddTemperatureEvent(predictedValue.Temperature - config.LowerTemperatureWarningLimit);
        }

        return Task.FromResult(microclimateEventBuilder.Build());
    }

    private async Task<PredictedValue> GetPrediction(Feature feature)
    {
        return await _mediator.Send(new GetPredictionQuery(feature));
    }

    private async Task<PredictedValue?> TryGetLastPrediction()
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