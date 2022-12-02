using ClimateControlSystem.Shared;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class AddAccuracyCommand : IRequest<bool>
    {
        public AccuracyData Data { get; init; }
    }
}
