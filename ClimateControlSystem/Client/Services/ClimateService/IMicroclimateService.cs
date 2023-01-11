using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public interface IMicroclimateService
    {
        Task<List<Monitoring>> GetMonitoringsAsync(int countRecord);
        Task<List<MicroclimateData>> GetMicroclimatesDataAsync(int countRecord);
    }
}
