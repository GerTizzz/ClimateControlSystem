namespace Application.CreationalPatterns
{
    public sealed class WarningBuilder
    {
        private readonly Warning _warning;

        private bool _hasAnyEvent;

        public WarningBuilder()
        {
            _warning = new Warning(Guid.NewGuid());
        }

        public WarningBuilder AddTemperatureEvent(float value)
        {
            _warning.Temperature = value;

            _hasAnyEvent = true;

            return this;
        }

        public WarningBuilder AddHumidityEvent(float value)
        {
            _warning.Humidity = value;

            _hasAnyEvent = true;

            return this;
        }

        public Warning? Build()
        {
            if (_hasAnyEvent)
            {
                return _warning;
            }

            return null;
        }
    }
}
