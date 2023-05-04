﻿namespace Application.Helpers;

public sealed class ForecastsBuilder
{
    private readonly Forecast _forecast;

    public ForecastsBuilder()
    {
        _forecast = new Forecast(Guid.NewGuid());
    }

    public ForecastsBuilder AddTracedTime(DateTimeOffset? time)
    {
        _forecast.Time = time;

        return this;
    }

    public ForecastsBuilder AddPrediction(Label label)
    {
        _forecast.Label = label;

        return this;
    }

    public ForecastsBuilder AddFeature(Feature feature)
    {
        _forecast.Feature = feature;

        return this;
    }

    public ForecastsBuilder AddActualData(Fact? actualData)
    {
        _forecast.Fact = actualData;

        return this;
    }

    public ForecastsBuilder AddError(Error? accuracy)
    {
        _forecast.Error = accuracy;

        return this;
    }

    public ForecastsBuilder AddWarning(Warning? microclimatesEvents)
    {
        _forecast.Warning = microclimatesEvents;

        return this;
    }

    public Forecast Build()
    {
        return _forecast;
    }
}