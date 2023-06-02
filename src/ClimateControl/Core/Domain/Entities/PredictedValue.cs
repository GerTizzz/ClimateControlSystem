using Domain.Primitives;

namespace Domain.Entities;

public sealed class PredictedValue : Entity
{
    public float Value { get; set; }

    public Guid ForecastId { get; set; }
    public Forecast Forecast { get; set; }

    public Guid? WarningId { get; set; }
    public Warning? Warning { get; set; }

    public PredictedValue(Guid id) : base(id)
    {

    }
}