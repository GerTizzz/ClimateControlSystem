using Domain.Entities;
using Domain.Primitives;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ForecastsRepository : IForecastsRepository
{
    private readonly MonitoringDatabaseContext _context;

    public ForecastsRepository(MonitoringDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Forecast>> GetForecastsAsync(IDbRangeRequest rangeRequestLimits)
    {
        try
        {
            var forecasts = await _context.Forecasts
                .Where(forecast => forecast.Predictions != null && forecast.Predictions.Any())
                .Include(forecast => forecast.Predictions)
                    .ThenInclude(prediction => prediction.Warning)
                .Include(forecast => forecast.Feature)
                .OrderByDescending(forecast => forecast.Time)
                .Skip(rangeRequestLimits.Start)
                .Take(rangeRequestLimits.Count)
                .ToListAsync();

            return forecasts;
        }
        catch
        {
            return Enumerable.Empty<Forecast>();
        }
    }

    public async Task<long> GetForecastsCountAsync()
    {
        return await _context.Forecasts
            .LongCountAsync(forecast => forecast.Predictions != null && forecast.Predictions.Any());
    }

    public async Task<IEnumerable<Feature>> GetLastFeatures(IDbRangeRequest rangeRequestLimits)
    {
        try
        {
            var features = await _context.Forecasts
                .Include(forecast => forecast.Feature)
                .OrderByDescending(forecast => forecast.Time)
                .Skip(rangeRequestLimits.Start)
                .Take(rangeRequestLimits.Count)
                .Where(forecast => forecast.Feature != null)
                .Select(forecast => forecast.Feature)
                .ToListAsync();

            return features;
        }
        catch
        {
            return Enumerable.Empty<Feature>();
        }
    }

    public async Task<bool> SaveForecastAsync(Forecast forecast)
    {
        _context.Forecasts.Add(forecast);

        await _context.SaveChangesAsync();

        return true;
    }
}