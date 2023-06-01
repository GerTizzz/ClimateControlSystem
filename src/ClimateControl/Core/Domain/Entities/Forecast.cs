using Domain.Primitives;

namespace Domain.Entities;

public sealed class Forecast : Entity
{
    public DateTimeOffset? Time { get; set; }

    public Guid? FactId { get; set; }
    public ActualValue? Fact { get; set; }

    public Guid? LabelId { get; set; }
    public PredictedValue? Label { get; set; }

    public Guid? WarningId { get; set; }
    public Warning? Warning { get; set; }
        
    public Guid? FeatureId { get; set; }
    public Feature? Feature { get; set; }

    public Forecast(Guid id)
    {
        Id = id;
    }
}