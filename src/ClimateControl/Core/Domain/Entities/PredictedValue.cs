using Domain.Primitives;

namespace Domain.Entities;

public sealed class PredictedValue : Entity
{
    public float[] Values { get; set; }

    public PredictedValue(Guid id) : base(id)
    {

    }
}