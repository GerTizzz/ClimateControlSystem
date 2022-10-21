using ClimateControlSystem.Server.Resources;
using MediatR;

namespace ClimateControlSystem.Server.Services.Queries
{
    public record GetPredictionQuery() : IRequest<PredictionResult>
    {
        public PredictionRequest Data { get; init; }
    }
}
