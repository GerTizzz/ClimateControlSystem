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

        public async Task<IEnumerable<BaseMonitoringDto>> GetBaseMonitoringsAsync(int start = 0, int count = 25)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<BaseMonitoringDto>>($"api/monitoring/monitorings/{start}/{count}");

                return result?.Reverse<BaseMonitoringDto>().ToList() ?? Enumerable.Empty<BaseMonitoringDto>();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return Enumerable.Empty<BaseMonitoringDto>();
        }

        public async Task<IEnumerable<MonitoringWithAccuracyDto>> GetMonitoringsWithAccuraciesAsync(int start = 0, int count = 25)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<MonitoringWithAccuracyDto>>($"api/monitoring/monitoringswithaccuracies/{start}/{count}");

                return result?.Reverse<MonitoringWithAccuracyDto>().ToList() ?? Enumerable.Empty<MonitoringWithAccuracyDto>();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return Enumerable.Empty<MonitoringWithAccuracyDto>();
        }

        public async Task<IEnumerable<ForecastingDto>> GetForecastingsAsync(int offsetFromTheEnd, int count)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<ForecastingDto>>($"api/monitoring/monitoringsforecastings/{offsetFromTheEnd}/{count}");
                
                return result ?? Enumerable.Empty<ForecastingDto>();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return Enumerable.Empty<ForecastingDto>();
        }

        public async Task<IEnumerable<MonitoringsEventsDto>> GetEventsAsync(int start = 0, int count = 25)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<MonitoringsEventsDto>>($"api/monitoring/monitoringsevents/{start}/{count}");
                
                return result ?? Enumerable.Empty<MonitoringsEventsDto>();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return Enumerable.Empty<MonitoringsEventsDto>();
        }
    }
}
