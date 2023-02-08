using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Hubs
{
    public interface IMonitoringHub
    {
        Task SendMonitoringToWebClients(MonitoringData monitoring);
    }
}