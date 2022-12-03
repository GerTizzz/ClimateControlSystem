using ClimateControlSystem.Shared;
using Microsoft.AspNetCore.SignalR;

namespace ClimateControlSystem.Server.Hubs
{
    public class MonitoringHub : Hub
    {
        public async Task SendMonitoringData(Prediction prediction)
        {
            await Clients.All.SendAsync("GetMonitoringData", prediction);
        }
    }
}
