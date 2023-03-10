using Domain.Entities;
using MediatR;

namespace WebApi.Services.MediatR.Commands
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
