using Domain.Enumerations;
using Domain.Primitives;

namespace Domain.Entities;

public sealed class Warning : Entity
{
    public WarningType Type { get; set; }
    public string Message { get; set; }
    
    public Warning(Guid id, string message, WarningType type) : base(id)
    {
        Message = message;
        Type = type;
    }
}