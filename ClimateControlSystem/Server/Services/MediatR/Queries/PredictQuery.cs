using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared;
using MediatR;

namespace ClimateControlSystem.Server.Services.Queries
{
    public record PredictQuery() : IRequest<PredictionResultData>
    {
        public SensorsData Data { get; init; }
    }
}
