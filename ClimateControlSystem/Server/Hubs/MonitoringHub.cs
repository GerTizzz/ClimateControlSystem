using AutoMapper;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared.SendToClient;
using Microsoft.AspNetCore.SignalR;

namespace ClimateControlSystem.Server.Hubs
{
    public class MonitoringHub : Hub, IMonitoringHub
    {
        private readonly IMapper _mapper;

        public MonitoringHub(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task SendMonitoringToWebClients(MonitoringData monitoring)
        {
            var dataToSend = _mapper.Map<MonitoringResponse>(monitoring);

            if (Clients is not null)
            {
                await Clients.All.SendAsync("GetMonitoringResponse", dataToSend);
            }
        }
    }
}
