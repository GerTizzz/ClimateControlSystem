using ClimateControlSystem.Client.Services.AuthenticationService;
using ClimateControlSystem.Shared;
using System.Net.Http.Json;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public class ClimateService : IClimateService
    {
        private const int RecordsCount = 25;

        private readonly HttpClient _httpClient;
        private readonly IAuthenticationService _authService;

        public ClimateService(HttpClient httpClient, IAuthenticationService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<List<Prediction>> GetPredictionsAsync(int countRecords)
        {
            if (countRecords > RecordsCount)
            {
                countRecords = RecordsCount;
            }

            try
            {
                string urlRequest = $"api/climate/predictions/{countRecords}";
                var result = await _httpClient.GetFromJsonAsync<List<Prediction>>(urlRequest);
                return result;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }
            
            return new List<Prediction>();
        }

        public async Task<List<Prediction>> GetMonitoringsAsync(int countRecords)
        {
            if (countRecords > RecordsCount)
            {
                countRecords = RecordsCount;
            }

            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<Prediction>>($"api/climate/monitorings/{countRecords}") ?? new List<Prediction>();
                return result;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return new List<Prediction>();
        }
    }
}
