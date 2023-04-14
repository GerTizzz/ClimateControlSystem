using Shared.Dtos;
using System.Net.Http.Json;
using WebClient.Services.AuthenticationService;

namespace WebClient.Services.WarningService;

public sealed class WarningService : IWarningService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthenticationService _authService;

    public WarningService(HttpClient httpClient, IAuthenticationService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<List<WarningDto>> GetWarningsAsync(int start = 0, int count = 25)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<WarningDto>>($"api/warnings/interval/{start}/{count}");

            return result ?? Enumerable.Empty<WarningDto>().ToList();
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
            {
                await _authService.Logout();
            }
        }

        return Enumerable.Empty<WarningDto>().ToList();
    }

    public async Task<long> GetWarningsCountAsync()
    {
        try
        {
            var totalCount = await _httpClient.GetFromJsonAsync<long>($"api/warnings/count");

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
}