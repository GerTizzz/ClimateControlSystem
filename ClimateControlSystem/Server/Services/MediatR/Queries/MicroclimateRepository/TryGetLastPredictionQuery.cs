using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository
{
    public sealed class TryGetLastPredictionQuery : IRequest<Prediction?>
    {
    }
}
