using AutoMapper;
using ClimateControlSystem.Server.Hubs;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using ClimateControlSystem.Shared.SendToClient;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class SendMonitoringHandler : IRequestHandler<SendMonitoringCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IMonitoringHub _monitoringHub;

        public SendMonitoringHandler(IMapper mapper, IMonitoringHub monitoringHub)
        {
            _mapper = mapper;
            _monitoringHub = monitoringHub;
        }

        public async Task<bool> Handle(SendMonitoringCommand request, CancellationToken cancellationToken)
        {
            var monitoring = _mapper.Map<MonitoringResponse>(request.Monitoring);

            if (monitoring is not null)
            {
                await _monitoringHub.SendMonitoringToWebClients(monitoring);

                return true;
            }

            return false;
        }
    }
}
