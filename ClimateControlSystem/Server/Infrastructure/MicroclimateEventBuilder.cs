using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Infrastructure
{
    public sealed class MicroclimateEventBuilder
    {
        private readonly MicroclimateEvent _microclimateEvent;

        private bool _isAnyEvents;

        public MicroclimateEventBuilder()
        {
            _microclimateEvent = new MicroclimateEvent();
        }

        public MicroclimateEventBuilder AddTemperatureEvent(float value)
        {
            _microclimateEvent.TemperatureValue = value;

            _isAnyEvents = true;

            return this;
        }

        public MicroclimateEventBuilder AddHumidityEvent(float value)
        {
            _microclimateEvent.HumidityValue = value;

            _isAnyEvents = true;

            return this;
        }

        public MicroclimateEvent? Build()
        {
            if (_isAnyEvents)
            {
                return _microclimateEvent;
            }

            return null;
        }
    }
}
