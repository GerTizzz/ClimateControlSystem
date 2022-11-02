using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public interface IClimateService
    {
        delegate void DataCompletedEventArgs();
        public event DataCompletedEventArgs DataCompleted;
        List<MonitoringData> ClimateRecords { get; set; }
        Task<List<MonitoringData>> GetClimateRecordsAsync(int climateRecordsAmount);
    }
}
