using MediatR;

namespace Application.MediatR.PredictionEngine;

public sealed class GetPredictionQuery : IRequest<Label>
{
    public Feature Feature { get; }

    public GetPredictionQuery(Feature feature)
    {
        Feature = feature;
    }
}