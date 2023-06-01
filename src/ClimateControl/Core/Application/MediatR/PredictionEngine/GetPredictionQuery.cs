using MediatR;

namespace Application.MediatR.PredictionEngine;

public sealed class GetPredictionQuery : IRequest<List<PredictedValue>>
{
    public IEnumerable<Feature> Features { get; }

    public GetPredictionQuery(IEnumerable<Feature> features)
    {
        Features = features;
    }
}