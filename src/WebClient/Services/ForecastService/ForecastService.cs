using System.Net.Http.Json;
using Shared.Dtos;
using WebClient.Services.AuthenticationService;

namespace WebClient.Services.ForecastService;

public class ForecastService : IForecastService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthenticationService _authService;

    public ForecastService(HttpClient httpClient, IAuthenticationService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<long> GetForecastsCountAsync()
    {
        try
        {
            var totalCount = await _httpClient.GetFromJsonAsync<long>($"api/forecasts/count");

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

    public async Task<ForecastDto?> GetForecastAsync(int number)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<ForecastDto>($"api/forecasts/{number}");

            return result;
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
            {
                await _authService.Logout();
            }
        }

        return null;
    }

    public async Task<List<ForecastDto>> GetForecastsAsync(int start = 0, int count = 25)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<ForecastDto>>($"api/forecasts/interval/{start}/{count}");

            return result ?? Enumerable.Empty<ForecastDto>().ToList();
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
            {
                await _authService.Logout();
            }
        }

        return Enumerable.Empty<ForecastDto>().ToList();
    }

    public async Task<List<FeaturesDto>> GetFeaturesAsync(int start = 0, int count = 144)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<FeaturesDto>>($"api/forecasts/features/{start}/{count}");

            return result ?? Enumerable.Empty<FeaturesDto>().ToList();
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
            {
                await _authService.Logout();
            }
        }

        return Enumerable.Empty<FeaturesDto>().ToList();
    }
}