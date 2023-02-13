using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public sealed class SaveMonitoringCommand : IRequest<bool>
    {
        public Monitoring Monitoring { get; init; }
    }
}
