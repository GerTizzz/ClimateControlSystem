using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public interface IClimateService
    {
        List<MonitoringData> ClimateRecords { get; set; }
        Task<List<MonitoringData>> GetClimateRecordsAsync(int countRecord);
    }
}
