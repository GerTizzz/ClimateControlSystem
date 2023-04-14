using Domain.Primitives;

namespace Domain.Entities;

public sealed class Error : Entity
{
    public float Temperature { get; }
    public float Humidity { get; }

    public Error(Guid id, float temperature, float humidity) : base(id)
    {
        Temperature = temperature;
        Humidity = humidity;
    }
}