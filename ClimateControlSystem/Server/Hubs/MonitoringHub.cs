using ClimateControlSystem.Shared;
using Microsoft.AspNetCore.SignalR;

namespace ClimateControlSystem.Server.Hubs
{
    public class MonitoringHub : Hub
    {
        public async Task SendMonitoringData(MonitoringData monitoring)
        {
            await Clients.All.SendAsync("GetMonitoringData", monitoring);
        }
    }
}
