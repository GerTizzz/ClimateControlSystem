using MediatR;

namespace Application.MediatR.ForecastsRepository;

public sealed class TryGetLastPredictionQuery : IRequest<PredictedValue?>
{
}