using ClimateControlSystem.Shared;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class UpdatePredictionAccuraciesCommand : IRequest<bool>
    {
        public MonitoringData Data { get; init; }
    }
}
