using Domain.Enumerations;
using Domain.Primitives;

namespace Domain.Entities;

public sealed class Warning : Entity
{
    public WarningType Type { get; }
    public string Message { get; }
    
    public Warning(Guid id, string message, WarningType type) : base(id)
    {
        Message = message;
        Type = type;
    }
}