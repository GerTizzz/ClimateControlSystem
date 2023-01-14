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

        public async Task<IReadOnlyCollection<MonitoringResponse>> GetMonitoringsDataAsync(int start = 0, int count = 25)
        {
            count = CheckCountParameter(count);

            try
            {
                string urlRequest = $"api/microclimate/monitorings/{count}";
                var result = await _httpClient.GetFromJsonAsync<List<MonitoringResponse>>(urlRequest);
                return result;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }
            
            return new List<MonitoringResponse>();
        }

        public async Task<IReadOnlyCollection<MicroclimateResponse>> GetMicroclimatesDataAsync(int start = 0, int count = 25)
        {
            count = CheckCountParameter(count);

            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<MicroclimateResponse>>($"api/microclimate/microclimates/{count}") ?? new List<MicroclimateResponse>();
                return result;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return new List<MicroclimateResponse>();
        }

        public async Task<IReadOnlyCollection<TemperatureEventResponse>> GetTemperatureEventDataAsync(int start = 0, int count = 25)
        {
            count = CheckCountParameter(count);

            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<TemperatureEventResponse>>($"api/microclimate/temperatureevents/{start}/{count}") ?? new List<TemperatureEventResponse>();
                return result;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return new List<TemperatureEventResponse>();
        }

        public async Task<IReadOnlyCollection<HumidityEventResponse>> GetHumidityEventDataAsync(int start = 0, int count = 25)
        {
            count = CheckCountParameter(count);

            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<HumidityEventResponse>>($"api/microclimate/humidityevents/{start}/{count}") ?? new List<HumidityEventResponse>();
                return result;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return new List<HumidityEventResponse>();
        }

        private int CheckCountParameter(int count)
        {
            if (count > RecordsCount)
            {
                return RecordsCount;
            }

            return count;
        }
    }
}
