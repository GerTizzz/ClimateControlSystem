using ClimateControlSystem.Server.Resources;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class SavePredictionCommand : IRequest<int>
    {
        public ClimateRecord Data { get; init; }
    }
}
