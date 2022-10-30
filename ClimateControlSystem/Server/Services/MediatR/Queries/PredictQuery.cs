using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.Queries
{
    public record PredictQuery() : IRequest<PredictionResult>
    {
        public IncomingMonitoringData Data { get; init; }
    }
}
