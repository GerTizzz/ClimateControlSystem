using AutoMapper;
using ClimateControlSystem.Server.Hubs;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using ClimateControlSystem.Shared.SendToClient;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class SendMonitoringHandler : IRequestHandler<SendMonitoringCommand, bool>
    {
        private readonly IMonitoringHub _monitoringHub;

        public SendMonitoringHandler(IMapper mapper, IMonitoringHub monitoringHub)
        {
            _monitoringHub = monitoringHub;
        }

        public async Task<bool> Handle(SendMonitoringCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _monitoringHub.SendMonitoringToWebClients(request.Monitoring);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
