using ClimateControlSystem.Shared.Common;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ClimateControlSystem.Client.Services.UsersService
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public List<UserDTO> Users { get; set; } = new List<UserDTO>();

        public UsersService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<UserDTO>($"api/user/{id}");

            return result;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserDTO>>("api/user");
            
            return result;
        }

        public async Task CreateUser(UserDTO user)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user", user);

            await SetUsers(result);
        }

        public async Task UpdateUser(UserDTO user)
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
            var response = await result.Content.ReadFromJsonAsync<List<UserDTO>>();

            if (response is null)
            {
                return;
            }

            Users = response;

            _navigationManager.NavigateTo("users");
        }
    }
}
