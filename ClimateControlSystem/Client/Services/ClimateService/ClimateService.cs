using ClimateControlSystem.Shared;
using System.Net.Http.Json;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public class ClimateService : IClimateService
    {
        private readonly HttpClient _httpClient;

        public List<MonitoringData> ClimateRecords { get; set; } = new List<MonitoringData>();

        public ClimateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task GetClimateRecords(int climateRecordsAmount)
        {
            var result = await _httpClient.GetFromJsonAsync<List<MonitoringData>>($"api/climate/{climateRecordsAmount}") ?? new List<MonitoringData>();

            ClimateRecords = result;
        }
    }
}
