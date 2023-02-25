using ClimateControlSystem.Client.Services.AuthenticationService;
using ClimateControlSystem.Shared.Responses;
using System.Net.Http.Json;
using ClimateControlSystem.Client.Services.MicroclimateService;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public class MicroclimateService : IMicroclimateService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthenticationService _authService;

        public MicroclimateService(HttpClient httpClient, IAuthenticationService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<int> GetMonitoringsCount()
        {
            try
            {
                var totalCount = await _httpClient.GetFromJsonAsync<int>($"api/microclimate/monitoringscount");

                return totalCount;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return 0;
        }

        public async Task<int> GetMicroclimatesCount()
        {
            try
            {
                var totalCount = await _httpClient.GetFromJsonAsync<int>($"api/microclimate/microclimatescount");

                return totalCount;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return 0;
        }

        public async Task<List<BaseMonitoringDto>> GetBaseMonitoringsAsync(int start = 0, int count = 25)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<BaseMonitoringDto>>($"api/microclimate/monitorings/{start}/{count}");

                return result.Reverse<BaseMonitoringDto>().ToList();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }
            
            return new List<BaseMonitoringDto>();
        }

        public async Task<List<MonitoringWithAccuracyDto>> GetMonitoringsWithAccuraciesAsync(int start = 0, int count = 25)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<MonitoringWithAccuracyDto>>($"api/microclimate/monitoringswithaccuracies/{start}/{count}");

                return result.Reverse<MonitoringWithAccuracyDto>().ToList();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return new List<MonitoringWithAccuracyDto>();
        }

        public async Task<List<ForecastingDto>> GetMicroclimatesAsync(int offsetFromTheEnd, int count)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<ForecastingDto>>($"api/microclimate/microclimates/{offsetFromTheEnd}/{count}") ?? new List<ForecastingDto>();
                return result;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return new List<ForecastingDto>();
        }

        public async Task<List<MonitoringsEventsDto>> GetMonitoringEventsAsync(int start = 0, int count = 25)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<MonitoringsEventsDto>>($"api/microclimate/monitoringevents/{start}/{count}") ?? new List<MonitoringsEventsDto>();
                return result;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return new List<MonitoringsEventsDto>();
        }
    }
}
