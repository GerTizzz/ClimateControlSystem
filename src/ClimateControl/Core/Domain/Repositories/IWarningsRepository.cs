using Domain.Entities;
using Domain.Primitives;

namespace Domain.Repositories;

public interface IWarningsRepository
{
    Task<long> GetWarningsCountAsync();

    Task<IEnumerable<Forecast>> GetWarningsAsync(IDbRequest requestLimits);
}