using Domain.Primitives;

namespace Domain.Entities;

public sealed class Forecast : Entity
{
    public DateTimeOffset Time { get; set; }

    public List<PredictedValue>? Predictions { get; set; }
        
    public Guid? FeatureId { get; set; }
    public Feature? Feature { get; set; }

    public Forecast(Guid id) : base(id)
    {
        Id = id;
    }
}