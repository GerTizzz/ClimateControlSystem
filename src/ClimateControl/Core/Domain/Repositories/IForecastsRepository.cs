using Domain.Entities;
using Domain.Primitives;

namespace Domain.Repositories;

public interface IForecastsRepository
{
    Task<bool> SaveForecastAsync(Forecast forecast);

    Task<long> GetForecastsCountAsync();

    Task<IEnumerable<Feature>> GetLastFeatures(IDbRangeRequest rangeRequestLimits);
    
    Task<IEnumerable<Forecast>> GetForecastsAsync(IDbRangeRequest rangeRequestLimits);
}