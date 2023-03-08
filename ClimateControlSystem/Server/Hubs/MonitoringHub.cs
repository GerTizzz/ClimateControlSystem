using AutoMapper;
using ClimateControl.Shared.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace ClimateControl.Server.Hubs
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
            var dataToSend = _mapper.Map<MonitoringWithAccuracyDto>(monitoring);

            if (Clients is not null)
            {
                await Clients.All.SendAsync("GetMonitoringResponse", dataToSend);
            }
        }
    }
}
