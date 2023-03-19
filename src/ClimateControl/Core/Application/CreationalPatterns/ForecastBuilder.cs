namespace Application.CreationalPatterns
{
    public sealed class ForecastBuilder
    {
        private readonly Forecast _forecast;

        public ForecastBuilder()
        {
            _forecast = new Forecast(Guid.NewGuid());
        }

        public ForecastBuilder AddTracedTime(DateTimeOffset? time)
        {
            _forecast.Time = time;

            return this;
        }

        public ForecastBuilder AddPrediction(Label label)
        {
            _forecast.Label = label;

            return this;
        }

        public ForecastBuilder AddFeature(Feature feature)
        {
            _forecast.Feature = feature;

            return this;
        }

        public ForecastBuilder AddActualData(Fact? actualData)
        {
            _forecast.Fact = actualData;

            return this;
        }

        public ForecastBuilder AddError(Error? accuracy)
        {
            _forecast.Error = accuracy;

            return this;
        }

        public ForecastBuilder AddWarning(Warning? microclimatesEvents)
        {
            _forecast.Warning = microclimatesEvents;

            return this;
        }

        public Forecast Build()
        {
            return _forecast;
        }
    }
}
