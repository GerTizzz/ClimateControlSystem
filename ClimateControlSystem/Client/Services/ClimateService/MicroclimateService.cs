using ClimateControlSystem.Client.Services.AuthenticationService;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.SendToClient;
using System.Net.Http.Json;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public class MicroclimateService : IMicroclimateService
    {
        private const int RecordsCount = 25;

        private readonly HttpClient _httpClient;
        private readonly IAuthenticationService _authService;

        public MicroclimateService(HttpClient httpClient, IAuthenticationService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<List<Monitoring>> GetMonitoringsAsync(int countRecords)
        {
            if (countRecords > RecordsCount)
            {
                countRecords = RecordsCount;
            }

            try
            {
                string urlRequest = $"api/microclimate/monitorings/{countRecords}";
                var result = await _httpClient.GetFromJsonAsync<List<Monitoring>>(urlRequest);
                return result;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }
            
            return new List<Monitoring>();
        }

        public async Task<List<MicroclimateData>> GetMicroclimatesDataAsync(int countRecords)
        {
            if (countRecords > RecordsCount)
            {
                countRecords = RecordsCount;
            }

            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<MicroclimateData>>($"api/microclimate/microclimates/{countRecords}") ?? new List<MicroclimateData>();
                return result;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return new List<MicroclimateData>();
        }
    }
}
