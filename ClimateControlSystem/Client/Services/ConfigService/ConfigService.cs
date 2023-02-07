using ClimateControlSystem.Client.Services.AuthenticationService;
using ClimateControlSystem.Shared.Common;
using System.Net.Http.Json;

namespace ClimateControlSystem.Client.Services.ConfigService
{
    public class ConfigService : IConfigService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthenticationService _authService;

        public ConfigService(HttpClient httpClient, IAuthenticationService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<ConfigResponse> GetConfigAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ConfigResponse>($"api/config/") ?? GetDefaultConfig();
                return result;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return GetDefaultConfig();
        }

        public async Task<bool> UpdateConfigAsync(ConfigResponse config)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync($"api/config/", config);
                return await UpdateConfigResponse(result);
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }

                return false;
            }
        }

        private async Task<bool> UpdateConfigResponse(HttpResponseMessage result)
        {
            return await result.Content.ReadFromJsonAsync<bool>();
        }

        private static ConfigResponse GetDefaultConfig()
        {
            return new ConfigResponse()
            {
                UpperTemperatureWarningLimit = 24f,
                LowerTemperatureWarningLimit = 16f,
                UpperHumidityWarningLimit = 21f,
                LowerHumidityWarningLimit = 10f,
                PredictionTimeIntervalSeconds = 5
            };
        }
    }
}
