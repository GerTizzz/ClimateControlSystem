using ClimateControlSystem.Shared;
using Microsoft.AspNetCore.SignalR;

namespace ClimateControlSystem.Server.Hubs
{
    public class MonitoringHub : Hub
    {
        public async Task SendMonitoringData(MonitoringResponse prediction)
        {
            await Clients.All.SendAsync("GetMonitoringData", prediction);
        }
    }
}
