using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public interface IClimateService
    {
        List<MonitoringData> ClimateRecords { get; set; }
        Task GetClimateRecords(int climateRecordsAmount);
    }
}
