namespace ClimateControlSystem.Shared.SendToClient
{
    public record class MicroclimateEventResponse
    {
        public float? TemperatureValue { get; init; }
        public float? HumidityValue { get; init; }
    }
}
