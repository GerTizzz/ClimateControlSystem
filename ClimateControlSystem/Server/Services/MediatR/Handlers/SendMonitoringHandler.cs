using AutoMapper;
using ClimateControl.Server.Hubs;
using ClimateControl.Server.Services.MediatR.Commands;
using ClimateControl.Shared.Dtos;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ClimateControl.Server.Services.MediatR.Handlers
{
    public sealed class SendMonitoringHandler : IRequestHandler<SendMonitoringCommand, bool>
    {
        private readonly IHubContext<MonitoringHub> _monitoringHub;
        private readonly IMapper _mapper;

        public SendMonitoringHandler(IMapper mapper, IHubContext<MonitoringHub> monitoringHub)
        {
            _mapper = mapper;
            _monitoringHub = monitoringHub;
        }

        public async Task<bool> Handle(SendMonitoringCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var monitoring = _mapper.Map<MonitoringWithEventsDto>(request.Monitoring);

                await _monitoringHub.Clients.All.SendAsync("GetMonitoringResponse", monitoring);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
