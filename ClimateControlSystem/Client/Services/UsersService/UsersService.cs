using ClimateControlSystem.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ClimateControlSystem.Client.Services.UsersService
{
    public class UsersService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public List<UserModel> Users { get; set; } = new List<UserModel>();

        public UsersService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task<UserModel> GetUser(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<UserModel>($"api/user/{id}");

            return result;
        }

        public async Task GetUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserModel>>("api/user");
            if (result is not null)
            {
                Users = result;
            }
        }

        public async Task CreateUser(UserModel user)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user", user);

            await SetUsers(result);
        }

        public async Task UpdateUser(UserModel user)
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
            var response = await result.Content.ReadFromJsonAsync<List<UserModel>>();

            if (response is null)
            {
                return;
            }

            Users = response;

            _navigationManager.NavigateTo("users");
        }
    }
}
