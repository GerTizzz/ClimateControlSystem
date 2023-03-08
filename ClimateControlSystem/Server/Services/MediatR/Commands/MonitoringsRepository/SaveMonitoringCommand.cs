using MediatR;

namespace ClimateControl.Server.Services.MediatR.Commands.MonitoringsRepository
{
    public sealed class SaveMonitoringCommand : IRequest<bool>
    {
        public Monitoring Monitoring { get; }

        public SaveMonitoringCommand(Monitoring monitoring)
        {
            Monitoring = monitoring;
        }
    }
}
