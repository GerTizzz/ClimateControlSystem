using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Shared.Dtos;
using WebApi.Hubs;
using WebApi.Services.MediatR.Commands;

namespace WebApi.Services.MediatR.Handlers
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

                await _monitoringHub.Clients.All.SendAsync("GetMonitoringResponse", monitoring, cancellationToken: cancellationToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
