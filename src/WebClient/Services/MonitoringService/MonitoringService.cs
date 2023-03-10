using System.Net.Http.Json;
using Shared.Dtos;
using WebClient.Services.AuthenticationService;

namespace WebClient.Services.MonitoringService
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
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
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
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
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

                return result?.Reverse<BaseMonitoringDto>().ToList() ?? Enumerable.Empty<BaseMonitoringDto>().ToList();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return Enumerable.Empty<BaseMonitoringDto>().ToList();
        }

        public async Task<List<MonitoringWithAccuracyDto>> GetMonitoringsWithAccuraciesAsync(int start = 0, int count = 25)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<MonitoringWithAccuracyDto>>($"api/monitoring/monitoringswithaccuracies/{start}/{count}");

                return result?.Reverse<MonitoringWithAccuracyDto>().ToList() ?? Enumerable.Empty<MonitoringWithAccuracyDto>().ToList();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return Enumerable.Empty<MonitoringWithAccuracyDto>().ToList();
        }

        public async Task<List<ForecastingDto>> GetForecastingsAsync(int offsetFromTheEnd, int count)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<ForecastingDto>>($"api/monitoring/monitoringsforecastings/{offsetFromTheEnd}/{count}");

                return result ?? Enumerable.Empty<ForecastingDto>().ToList();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return Enumerable.Empty<ForecastingDto>().ToList();
        }

        public async Task<List<MonitoringsEventsDto>> GetEventsAsync(int start = 0, int count = 25)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<MonitoringsEventsDto>>($"api/monitoring/monitoringsevents/{start}/{count}");

                return result ?? Enumerable.Empty<MonitoringsEventsDto>().ToList();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return Enumerable.Empty<MonitoringsEventsDto>().ToList();
        }
    }
}
