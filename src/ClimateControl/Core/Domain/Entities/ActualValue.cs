using Domain.Primitives;

namespace Domain.Entities;

public sealed class ActualValue : Entity
{
    public float Temperature { get; set; }

    public ActualValue(Guid id) : base(id)
    {

    }
}