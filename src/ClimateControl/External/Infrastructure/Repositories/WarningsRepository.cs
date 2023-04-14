using Domain.Entities;
using Domain.Primitives;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class WarningsRepository : IWarningsRepository
{
    private readonly MonitoringDatabaseContext _context;

    public WarningsRepository(MonitoringDatabaseContext context)
    {
        _context = context;
    }

    public async Task<long> GetWarningsCountAsync()
    {
        return await _context.Forecasts
            .Include(monitoring => monitoring.Warning)
            .Where(monitoring => monitoring.Warning != null)
            .LongCountAsync();
    }

    public async Task<IEnumerable<Forecast>> GetWarningsAsync(IDbRequest requestLimits)
    {
        try
        {
            var warnings = await _context.Forecasts
                .Include(forecast => forecast.Warning)
                .OrderByDescending(forecast => forecast.Time)
                .Where(forecast => forecast.Warning != null)
                .Skip(requestLimits.Start)
                .Take(requestLimits.Count)
                .ToListAsync();

            return warnings;
        }
        catch
        {
            return Enumerable.Empty<Forecast>();
        }
    }
}