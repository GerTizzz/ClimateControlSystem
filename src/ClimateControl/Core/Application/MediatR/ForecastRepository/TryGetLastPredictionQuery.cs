using MediatR;

namespace Application.MediatR.ForecastRepository
{
    public sealed class TryGetLastPredictionQuery : IRequest<Label?>
    {
    }
}
