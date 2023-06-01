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
    private readonly IWarningsService _warningsService;
    private readonly IList<float> _featuresBuffer;

    public ForecastsService(IMediator mediator, IWarningsService warningsService)
    {
        _mediator = mediator;
        _warningsService = warningsService;
        _featuresBuffer = new List<float>();
    }

    public async Task<PredictedValue?> Predict(Feature feature)
    {
        var predictedValue = await GetPrediction(feature);
 
        AddNewFeaturesToBuffer(feature);

        var forecast = await GetForecast(feature, predictedValue);

        await ProcessMonitoringData(forecast);

        return predictedValue;
    }

    private async Task<Forecast> GetForecast(Feature feature, PredictedValue? predictedValue)
    {
        var actualValue = GetActualDataFromFeaturesData(feature);
        
        var warning = await _warningsService.GetWarning(predictedValue);

        var forecast = new ForecastsBuilder()
            .AddActualData(actualValue)
            .AddTracedTime(DateTimeOffset.Now)
            .AddWarning(warning)
            .AddPrediction(predictedValue)
            .AddFeature(feature)
            .Build();

        return forecast;
    }

    private void AddNewFeaturesToBuffer(Feature feature)
    {
        _featuresBuffer.Add(feature.TemperatureInside);
        _featuresBuffer.Add(feature.TemperatureOutside);
        _featuresBuffer.Add(feature.CoolingPower);
        
        while (_featuresBuffer.Count == TensorPredictionRequest.InputSize)
        {
            _featuresBuffer.RemoveAt(0);
        }
    }

    private async Task ProcessMonitoringData(Forecast forecast)
    {
        await SaveMonitoring(forecast);

        await SendMonitoringToClients(forecast);
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

    private async Task<PredictedValue?> GetPrediction(Feature feature)
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