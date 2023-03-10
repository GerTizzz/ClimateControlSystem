using Domain.Entities;
using MediatR;

namespace WebApi.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class TryGetLastPredictionQuery : IRequest<Prediction?>
    {
    }
}
