using ClimateControlSystem.Shared;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class AddPredictionCommand : IRequest<bool>
    {
        public PredictionData Data { get; init; }
    }
}
