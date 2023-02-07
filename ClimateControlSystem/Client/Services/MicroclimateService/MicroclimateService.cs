﻿using ClimateControlSystem.Client.Services.AuthenticationService;
using ClimateControlSystem.Shared.SendToClient;
using System.Net.Http.Json;

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

        public async Task<IReadOnlyCollection<PredictionResponse>> GetMonitoringsAsync(int start = 0, int count = 25)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<PredictionResponse>>($"api/microclimate/monitorings/{start}/{count}");

                return result.Reverse<PredictionResponse>().ToList();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode.HasValue && e.StatusCode.Value == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _authService.Logout();
                }
            }
            
            return new List<PredictionResponse>();
        }

        public async Task<IReadOnlyCollection<MicroclimateResponse>> GetMicroclimatesAsync(int offsetFromTheEnd, int count)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<MicroclimateResponse>>($"api/microclimate/microclimates/{offsetFromTheEnd}/{count}") ?? new List<MicroclimateResponse>();
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

        public async Task<IReadOnlyCollection<TemperatureEventResponse>> GetTemperatureEventsAsync(int start = 0, int count = 25)
        {
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

        public async Task<IReadOnlyCollection<HumidityEventResponse>> GetHumidityEventsAsync(int start = 0, int count = 25)
        {
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
    }
}
