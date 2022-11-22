using ClimateControlSystem.Shared;
using System.Net.Http.Json;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public class ClimateService : IClimateService
    {
        private const int RecordsCount = 25;

        private readonly HttpClient _httpClient;

        public List<MonitoringData> ClimateRecords { get; set; } = new List<MonitoringData>();

        public ClimateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MonitoringData>> GetClimateRecordsAsync(int countRecords)
        {
            if (countRecords > RecordsCount)
            {
                countRecords = RecordsCount;
            }

            var result = await _httpClient.GetFromJsonAsync<List<MonitoringData>>($"api/climate/{countRecords}") ?? new List<MonitoringData>();

            return result;
        }
    }
}
