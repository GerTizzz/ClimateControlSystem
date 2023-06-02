using Shared.Dtos;

namespace WebClient.Services.FeaturesService;

public interface IFeatureService
{
    Task<long> GetFeaturesCountAsync();

    Task<List<FeaturesDto>> GetFeaturesAsync(int start, int count);
}
