namespace ClimateControlSystem.Shared.SendToClient
{
    public readonly record struct TemperatureEventResponse
    {
        public float Value { get; init; }
    }
}
