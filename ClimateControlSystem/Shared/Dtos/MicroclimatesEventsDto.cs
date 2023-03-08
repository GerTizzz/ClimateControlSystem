namespace ClimateControl.Shared.Dtos
{
    public record class MicroclimatesEventsDto
    {
        public float? TemperatureValue { get; init; }
        public float? HumidityValue { get; init; }
    }
}
