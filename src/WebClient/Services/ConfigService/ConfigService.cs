using System.Net.Http.Json;
using Shared.Dtos;
using WebClient.Services.AuthenticationService;

namespace WebClient.Services.ConfigService;

public class ConfigService : IConfigService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthenticationService _authService;

    public ConfigService(HttpClient httpClient, IAuthenticationService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<ConfigsDto?> GetConfigAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<ConfigsDto>($"api/configs/");

            return result ?? throw new AggregateException("Config has not been recieved!");
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

    public async Task<bool> UpdateConfigAsync(ConfigsDto config)
    {
        try
        {
            var result = await _httpClient.PutAsJsonAsync($"api/configs/", config);

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
}