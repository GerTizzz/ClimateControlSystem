namespace ClimateControlSystem.Shared.Responses
{
    public record class MicroclimatesEventsDTO
    {
        public float? TemperatureValue { get; init; }
        public float? HumidityValue { get; init; }
    }
}
