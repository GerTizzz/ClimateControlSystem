using Domain.Primitives;

namespace Domain.Entities;

public sealed class PredictedValue : Entity
{
    public float Temperature { get; set; }
    public float Humidity { get; set; }

    public PredictedValue(Guid id) : base(id)
    {

    }
}