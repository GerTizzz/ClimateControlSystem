namespace ClimateControlSystem.Shared.SendToClient
{
    public record class MicroclimateEventDTO
    {
        public float? TemperatureValue { get; init; }
        public float? HumidityValue { get; init; }
    }
}
