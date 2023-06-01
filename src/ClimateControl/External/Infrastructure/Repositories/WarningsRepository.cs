using Domain.Entities;
using Domain.Enumerations;
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

    public async Task<Warning> GetWarningByType(WarningType type)
    {
        return await _context.Warnings.FirstAsync(warning => warning.Type == type);
    }

    public async Task<long> GetWarningsCountAsync()
    {
        return await _context.Forecasts
            .Include(monitoring => monitoring.Warning)
            .Where(monitoring => monitoring.Warning != null)
            .LongCountAsync();
    }

    public async Task<IEnumerable<Forecast>> GetWarningsAsync(IDbRangeRequest rangeRequestLimits)
    {
        try
        {
            var warnings = await _context.Forecasts
                .Include(forecast => forecast.Warning)
                .OrderByDescending(forecast => forecast.Time)
                .Where(forecast => forecast.Warning != null)
                .Skip(rangeRequestLimits.Start)
                .Take(rangeRequestLimits.Count)
                .ToListAsync();

            return warnings;
        }
        catch
        {
            return Enumerable.Empty<Forecast>();
        }
    }
}