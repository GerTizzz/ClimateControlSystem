using Blazored.LocalStorage;
using ClimateControlSystem.Client.Authentication;
using ClimateControlSystem.Shared.Common;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ClimateControlSystem.Client.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client,
            AuthenticationStateProvider authStateProvider,
            ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<bool> Login(UserDto userForAuthentication)
        {
            var authResult = await _client.PostAsJsonAsync("api/auth/login", userForAuthentication);
            var token = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode is false)
            {
                return false;
            }

            await _localStorage.SetItemAsync("authToken", token);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(token);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
