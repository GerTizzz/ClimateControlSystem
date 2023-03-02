using ClimateControlSystem.Server.Resources.Domain;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public sealed class SendMonitoringCommand : IRequest<bool>
    {
        public Monitoring Monitoring { get; }

        public SendMonitoringCommand(Monitoring monitoring)
        {
            Monitoring = monitoring;
        }
    }
}
