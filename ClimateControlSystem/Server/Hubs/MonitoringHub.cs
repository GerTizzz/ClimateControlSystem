using AutoMapper;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared.Responses;
using Microsoft.AspNetCore.SignalR;

namespace ClimateControlSystem.Server.Hubs
{
    public class MonitoringHub : Hub
    {
        private readonly IMapper _mapper;

        public MonitoringHub(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task SendMonitoringToWebClients(Monitoring monitoring)
        {
            var dataToSend = _mapper.Map<MonitoringWithAccuracyDTO>(monitoring);

            if (Clients is not null)
            {
                await Clients.All.SendAsync("GetMonitoringResponse", dataToSend);
            }
        }
    }
}
