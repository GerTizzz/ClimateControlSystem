using Domain.Entities;
using Domain.Enumerations;
using Domain.Primitives;

namespace Domain.Repositories;

public interface IWarningsRepository
{
    Task<long> GetWarningsCountAsync();

    Task<IEnumerable<Forecast>> GetWarningsAsync(IDbRangeRequest rangeRequestLimits);

    Task<Warning> GetWarningByType(WarningType type);
}