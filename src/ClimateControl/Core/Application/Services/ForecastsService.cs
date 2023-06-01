using Application.Helpers;
using Application.MediatR.ForecastsRepository;
using Application.MediatR.PredictionEngine;
using Application.MediatR.SignalR;
using Application.Services.Strategies;
using Domain.Services;
using MediatR;

namespace Application.Services;

public class ForecastsService : IForecastsService
{
    private readonly IMediator _mediator;
    private readonly IWarningsService _warningsService;
    private readonly IFeaturesCollector _featuresCollector;

    public ForecastsService(IMediator mediator, IWarningsService warningsService, IFeaturesCollector featuresCollector)
    {
        _mediator = mediator;
        _warningsService = warningsService;
        _featuresCollector = featuresCollector;
    }

    public async Task<Forecast?> Predict(Feature feature)
    {
        _featuresCollector.AddNewData(feature);

        if (_featuresCollector.IsEnoughData)
        {
            var predictedValue = await GetPredictions(_featuresCollector.Features);

            var forecast = await GetForecast(feature, predictedValue);

            await ProcessMonitoringData(forecast);

            return forecast;
        }

        return null;
    }

    private async Task<Forecast> GetForecast(Feature feature, List<PredictedValue> predictedValue)
    {        
        foreach (var value in predictedValue)
        {
            value.Warning = await _warningsService.GetWarning(value);
        }

        var forecast = new ForecastsBuilder()
            .AddTracedTime(DateTimeOffset.Now)
            .AddPrediction(predictedValue)
            .AddFeature(feature)
            .Build();

        return forecast;
    }

    private async Task ProcessMonitoringData(Forecast forecast)
    {
        await SaveMonitoring(forecast);

        await SendMonitoringToClients(forecast);
    }

    private async Task<List<PredictedValue>> GetPredictions(IEnumerable<Feature> features)
    {
        return await _mediator.Send(new GetPredictionQuery(features));
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