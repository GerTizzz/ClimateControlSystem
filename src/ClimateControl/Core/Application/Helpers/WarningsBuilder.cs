namespace Application.Helpers;

public sealed class WarningsBuilder
{
    private readonly Warning _warning;

    private bool _hasAnyEvent;

    public WarningsBuilder()
    {
        _warning = new Warning(Guid.NewGuid());
    }

    public WarningsBuilder AddTemperatureEvent(float value)
    {
        _warning.Temperature = value;

        _hasAnyEvent = true;

        return this;
    }

    public WarningsBuilder AddHumidityEvent(float value)
    {
        _warning.Humidity = value;

        _hasAnyEvent = true;

        return this;
    }

    public Warning? Build()
    {
        return _hasAnyEvent ? _warning : null;
    }
}