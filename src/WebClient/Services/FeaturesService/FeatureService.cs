using Shared.Dtos;
using System.Net.Http.Json;
using WebClient.Services.AuthenticationService;

namespace WebClient.Services.FeaturesService
{
    public sealed class FeatureService : IFeatureService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthenticationService _authService;

        public FeatureService(HttpClient httpClient, IAuthenticationService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<long> GetFeaturesCountAsync()
        {
            try
            {
                var totalCount = await _httpClient.GetFromJsonAsync<long>($"api/features/count");

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

        public async Task<List<FeaturesDto>> GetFeaturesAsync(int start, int count)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<FeaturesDto>>($"api/features/range/{start}/{count}");

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
}
