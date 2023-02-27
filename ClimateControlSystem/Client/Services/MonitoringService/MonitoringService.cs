using ClimateControlSystem.Client.Services.AuthenticationService;
using ClimateControlSystem.Shared.Responses;
using System.Net.Http.Json;

namespace ClimateControlSystem.Client.Services.MonitoringService
{
    public class MonitoringService : IMonitoringService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthenticationService _authService;

        public MonitoringService(HttpClient httpClient, IAuthenticationService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<long> GetCountAsync()
        {
            try
            {
                var totalCount = await _httpClient.GetFromJsonAsync<long>($"api/monitoring/monitoringscount");

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

        public async Task<long> GetEventsCountAsync()
        {
            try
            {
                var totalCount = await _httpClient.GetFromJsonAsync<long>($"api/monitoring/monitoringseventscount");

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
                var result = await _httpClient.GetFromJsonAsync<List<BaseMonitoringDto>>($"api/monitoring/monitorings/{start}/{count}");

                if (result is null)
                {
                    return new List<BaseMonitoringDto>();
                }

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
                var result = await _httpClient.GetFromJsonAsync<List<MonitoringWithAccuracyDto>>($"api/monitoring/monitoringswithaccuracies/{start}/{count}");

                if (result is null)
                {
                    return new List<MonitoringWithAccuracyDto>();
                }

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

        public async Task<List<ForecastingDto>> GetForecastingsAsync(int offsetFromTheEnd, int count)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<ForecastingDto>>($"api/monitoring/monitoringsforecastings/{offsetFromTheEnd}/{count}") ?? new List<ForecastingDto>();
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

        public async Task<List<MonitoringsEventsDto>> GetEventsAsync(int start = 0, int count = 25)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<MonitoringsEventsDto>>($"api/monitoring/monitoringsevents/{start}/{count}") ?? new List<MonitoringsEventsDto>();
                
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
