using Domain.Primitives;

namespace Domain.Entities;

public sealed class Error : Entity
{
    public float Temperature { get; set; }

    public Error(Guid id) : base(id)
    {

    }
}