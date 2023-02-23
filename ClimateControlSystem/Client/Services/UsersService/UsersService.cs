﻿using ClimateControlSystem.Shared.Common;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ClimateControlSystem.Client.Services.UsersService
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        
        public UsersService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task<UserDto?> GetUser(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<UserDto>($"api/user/{id}");

            return result;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/user");

            return result;
        }

        public async Task CreateUser(UserDto user)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user", user);

            await SetUsers(result);
        }

        public async Task UpdateUser(UserDto user)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/user/{user.Id}", user);

            await SetUsers(result);
        }

        public async Task DeleteUser(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/user/{id}");
            
            await SetUsers(result);
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
}
