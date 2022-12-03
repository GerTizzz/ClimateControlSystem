using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries
{
    public class GetLastPredictionQuery : IRequest<PredictionResult>
    {
    }
}
