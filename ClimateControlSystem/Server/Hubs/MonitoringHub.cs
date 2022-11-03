using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Shared;
using Microsoft.AspNetCore.SignalR;

namespace ClimateControlSystem.Server.Hubs
{
    public class MonitoringHub : Hub, IMonitoringHub
    {
        public async Task SendMonitoringData(MonitoringData monitoring)
        {
            await Clients.All.SendAsync("GetMonitoringData", monitoring);
        }
    }
}
