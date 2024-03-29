﻿using ClimateControlSystem.Client.Services.AuthenticationService;
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

        public async Task<ConfigsDto> GetConfigAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ConfigsDto>($"api/config/");

                return result ?? throw new AggregateException("Config has not been recieved!");
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }

            return GetDefaultConfig();
        }

        public async Task<bool> UpdateConfigAsync(ConfigsDto config)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync($"api/config/", config);
                
                return await UpdateConfigResponse(result);
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }
            
            return false;
        }

        private static async Task<bool> UpdateConfigResponse(HttpResponseMessage result)
        {
            return await result.Content.ReadFromJsonAsync<bool>();
        }

        private static ConfigsDto GetDefaultConfig()
        {
            return new ConfigsDto()
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
