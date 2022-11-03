using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IMonitoringHub
    {
        Task SendMonitoringData(MonitoringData monitoring);
    }
}
