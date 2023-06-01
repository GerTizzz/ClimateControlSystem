namespace Application.Helpers;

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

    public ForecastsBuilder AddPrediction(PredictedValue? predictedValue)
    {
        _forecast.Label = predictedValue;

        return this;
    }

    public ForecastsBuilder AddFeature(Feature feature)
    {
        _forecast.Feature = feature;

        return this;
    }

    public ForecastsBuilder AddActualData(ActualValue? actualData)
    {
        _forecast.Fact = actualData;

        return this;
    }

    public ForecastsBuilder AddWarning(Warning? warning)
    {
        _forecast.Warning = warning;

        return this;
    }

    public Forecast Build()
    {
        return _forecast;
    }
}