using ClimateControlSystem.Shared;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries
{
    public class GetLastPredictionQuery : IRequest<PredictionData>
    {
    }
}
