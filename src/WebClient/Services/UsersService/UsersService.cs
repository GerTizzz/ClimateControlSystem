using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Shared.Dtos;
using WebClient.Services.AuthenticationService;

namespace WebClient.Services.UsersService;

public class UsersService : IUsersService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly IAuthenticationService _authService;

    public UsersService(HttpClient httpClient, NavigationManager navigationManager, IAuthenticationService authService)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _authService = authService;
    }

    public async Task<UserDto?> GetUser(Guid id)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<UserDto>($"api/users/{id}");

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

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/users");

            return result ?? Enumerable.Empty<UserDto>();
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
            {
                await _authService.Logout();
            }
        }

        return Enumerable.Empty<UserDto>();
    }

    public async Task CreateUser(UserDto user)
    {
        try
        {
            var result = await _httpClient.PostAsJsonAsync("api/users", user);

            await SetUsers(result);
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
            {
                await _authService.Logout();
            }
        }
    }

    public async Task UpdateUser(UserDto user)
    {
        try
        {
            var result = await _httpClient.PutAsJsonAsync($"api/users/{user.Id}", user);

            await SetUsers(result);
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
            {
                await _authService.Logout();
            }
        }
    }

    public async Task DeleteUser(Guid id)
    {
        try
        {
            var result = await _httpClient.DeleteAsync($"api/users/{id}");

            await SetUsers(result);
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode is System.Net.HttpStatusCode.Unauthorized)
            {
                await _authService.Logout();
            }
        }
    }

    private async Task SetUsers(HttpResponseMessage result)
    {
        var response = await result.Content.ReadFromJsonAsync<bool>();

        if (response)
        {
            _navigationManager.NavigateTo("users");
        }
    }
}