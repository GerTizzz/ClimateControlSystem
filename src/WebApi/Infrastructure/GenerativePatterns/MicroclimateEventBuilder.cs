using Domain.Entities;

namespace WebApi.Infrastructure.GenerativePatterns
{
    public sealed class MicroclimateEventBuilder
    {
        private readonly MicroclimatesEvents _microclimatesEvents;

        private bool _isAnyEvents;

        public MicroclimateEventBuilder()
        {
            _microclimatesEvents = new MicroclimatesEvents();
        }

        public MicroclimateEventBuilder AddTemperatureEvent(float value)
        {
            _microclimatesEvents.Temperature = value;

            _isAnyEvents = true;

            return this;
        }

        public MicroclimateEventBuilder AddHumidityEvent(float value)
        {
            _microclimatesEvents.Humidity = value;

            _isAnyEvents = true;

            return this;
        }

        public MicroclimatesEvents? Build()
        {
            if (_isAnyEvents)
            {
                return _microclimatesEvents;
            }

            return null;
        }
    }
}
