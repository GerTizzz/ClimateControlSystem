using ClimateControlSystem.Shared;
using System.Net.Http.Json;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public class ClimateService : IClimateService
    {
        private readonly HttpClient _httpClient;
        private IClimateService.DataCompletedEventArgs _dataCompleted;

        public List<MonitoringData> ClimateRecords { get; set; } = new List<MonitoringData>();

        public ClimateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public event IClimateService.DataCompletedEventArgs DataCompleted
        {
            add
            {
                _dataCompleted += value;
            }

            remove
            {
                _dataCompleted -= value;
            }
        }

        public async Task<List<MonitoringData>> GetClimateRecordsAsync(int climateRecordsAmount)
        {
            //string query = $"https://localhost:7286/api/climate/{climateRecordsAmount}";
            //Console.WriteLine(ClimateRecords.Count);

            var result = await _httpClient.GetFromJsonAsync<List<MonitoringData>>($"api/climate/{climateRecordsAmount}") ?? new List<MonitoringData>();

            _dataCompleted?.Invoke();

            return result;
        }
    }
}
