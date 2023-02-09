using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class SendMonitoringCommand : IRequest<bool>
    {
        public MonitoringWithEvents Monitoring { get; init; }
    }
}
