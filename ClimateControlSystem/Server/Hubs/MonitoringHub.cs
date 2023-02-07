using ClimateControlSystem.Shared.SendToClient;
using Microsoft.AspNetCore.SignalR;

namespace ClimateControlSystem.Server.Hubs
{
    public class MonitoringHub : Hub, IMonitoringHub
    {
        public MonitoringHub()
        {

        }

        public async Task SendMonitoringToWebClients(MonitoringResponse dataToSend)
        {
            await Clients.All.SendAsync("GetMonitoringResponse", dataToSend);
        }
    }
}
