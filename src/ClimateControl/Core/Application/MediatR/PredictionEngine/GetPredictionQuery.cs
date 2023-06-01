using MediatR;

namespace Application.MediatR.PredictionEngine;

public sealed class GetPredictionQuery : IRequest<PredictedValue>
{
    public Feature Feature { get; }

    public GetPredictionQuery(Feature feature)
    {
        Feature = feature;
    }
}