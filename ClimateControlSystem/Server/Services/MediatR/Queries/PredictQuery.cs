using ClimateControlSystem.Shared;
using MediatR;

namespace ClimateControlSystem.Server.Services.Queries
{
    public record PredictQuery() : IRequest<PredictionData>
    {
        public MonitoringData Data { get; init; }
    }
}
