using Domain.Primitives;

namespace Domain.Entities;

public sealed class Feature : Entity
{
    public float TemperatureInside { get; set; }
    public float TemperatureOutside { get; set; }
    public float CoolingPower { get; set; }

    public Feature(Guid id, float temperatureOutside, float temperatureInside, float coolingPower) : base(id)
    {
        TemperatureOutside = temperatureOutside;
        TemperatureInside = temperatureInside;
        CoolingPower = coolingPower;
    }
}