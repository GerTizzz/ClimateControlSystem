namespace Application.Helpers;

public sealed class ForecastsBuilder
{
    private readonly Forecast _forecast;

    public ForecastsBuilder()
    {
        _forecast = new Forecast(Guid.NewGuid());
    }

    public ForecastsBuilder AddTracedTime(DateTimeOffset time)
    {
        _forecast.Time = time;

        return this;
    }

    public ForecastsBuilder AddPrediction(List<PredictedValue> predictions)
    {
        _forecast.Predictions = predictions;

        foreach (PredictedValue prediction in predictions)
        {
            prediction.ForecastId = _forecast.Id;
        }

        return this;
    }

    public ForecastsBuilder AddFeature(Feature feature)
    {
        _forecast.Feature = feature;

        return this;
    }

    public Forecast Build()
    {
        return _forecast;
    }
}