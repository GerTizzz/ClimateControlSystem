using Domain.Entities;
using Domain.Primitives;

namespace Domain.Repositories
{
    public interface IForecastRepository
    {
        Task<bool> SaveForecastAsync(Forecast forecast);

        Task<long> GetWarningsCountAsync();

        Task<long> GetMicroclimatesEventsCountAsync();

        Task<Label?> TryGetLastPredictionAsync();

        Task<IEnumerable<Forecast>> GetForecastsAsync(IDbRequest requestLimits);
    }
}
