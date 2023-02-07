using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Server.Hubs
{
    public interface IMonitoringHub
    {
        Task SendMonitoringToWebClients(MonitoringResponse dataToSend);
    }
}