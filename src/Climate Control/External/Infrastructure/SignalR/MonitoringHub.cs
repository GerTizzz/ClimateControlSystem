using Microsoft.AspNetCore.SignalR;
using Shared.Dtos;

namespace Infrastructure.SignalR
{
    public class MonitoringHub : Hub
    {
        public async Task SendMonitoringToWebClients(MonitoringWithAccuracyDto dataToSend)
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("GetMonitoringResponse", dataToSend);
            }
        }
    }
}
