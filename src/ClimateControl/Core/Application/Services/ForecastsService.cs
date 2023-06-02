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
    private readonly IWarningsService _warningsService;

    public ForecastsService(IMediator mediator, IWarningsService warningsService)
    {
        _mediator = mediator;
        _warningsService = warningsService;
    }

    public async Task<Forecast?> Predict(Feature feature)
    {
        var predictedValue = await GetPredictions(feature);

        if (predictedValue is null)
        {
            return null;
        }

        var forecast = await GetForecast(feature, predictedValue);

        await ProcessMonitoringData(forecast);

        return forecast;
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

    private async Task<List<PredictedValue>> GetPredictions(Feature feature)
    {
        return await _mediator.Send(new GetPredictionQuery(feature));
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