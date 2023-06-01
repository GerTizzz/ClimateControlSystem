namespace Shared.Dtos;

public sealed record FeaturesDto
{
    public float TemperatureInside { get; set; }
    public float TemperatureOutside { get; set; }
    public float CoolingPower { get; set; }
}