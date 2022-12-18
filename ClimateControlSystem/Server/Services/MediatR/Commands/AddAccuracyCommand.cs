using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class AddAccuracyCommand : IRequest<bool>
    {
        public AccuracyData Accuracy { get; init; }
    }
}
