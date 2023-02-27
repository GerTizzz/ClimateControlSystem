using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class TryGetLastPredictionQuery : IRequest<Prediction?>
    {
    }
}
