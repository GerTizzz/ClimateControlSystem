using Microsoft.AspNetCore.SignalR;
using Shared.Dtos;

namespace Infrastructure.SignalR;

public class ForecastHub : Hub
{
    public async Task SendMonitoringToWebClients(ForecastDto dataToSend)
    {
        if (Clients is not null)
        {
            await Clients.All.SendAsync("GetNewForecast", dataToSend);
        }
    }
}