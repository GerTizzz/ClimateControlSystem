using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Shared.Dtos;

namespace WebApi.Hubs
{
    public class MonitoringHub : Hub
    {
        private readonly IMapper _mapper;

        public MonitoringHub(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task SendMonitoringToWebClients(Forecast forecast)
        {
            var dataToSend = _mapper.Map<MonitoringWithAccuracyDto>(forecast);

            if (Clients is not null)
            {
                await Clients.All.SendAsync("GetMonitoringResponse", dataToSend);
            }
        }
    }
}
