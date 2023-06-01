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
                .Include(forecast => forecast.Label)
                .Include(forecast => forecast.Fact)
                .Include(forecast => forecast.Warning)
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
        return await _context.Forecasts.LongCountAsync();
    }

    public async Task<bool> SaveForecastAsync(Forecast forecast)
    {
        if (await _context.Forecasts.AnyAsync() is false)
        {
            _context.Forecasts.Add(new Forecast(Guid.NewGuid())
            {
                Fact = forecast.Fact,
                Time = forecast.Time
            });

            await _context.SaveChangesAsync();
        }
        else
        {
            var lastForecasting = await _context.Forecasts
                .OrderByDescending(record => record.Time)
                .FirstOrDefaultAsync();
                    
            if (lastForecasting is not null)
            {
                if (forecast.Fact is not null)
                {
                    await _context.ActualValues.AddAsync(forecast.Fact);
                    await _context.SaveChangesAsync();
                }

                if (forecast.Warning is not null)
                {
                    await _context.Warnings.AddAsync(forecast.Warning);
                    await _context.SaveChangesAsync();
                }

                lastForecasting.Time = forecast.Time;
                lastForecasting.Fact = forecast.Fact;
                lastForecasting.Warning = forecast.Warning;

                await _context.SaveChangesAsync();
            }
        }

        var dateTime = forecast.Time?.AddSeconds(1) ?? DateTimeOffset.Now;

        await _context.Forecasts.AddAsync(new Forecast(forecast.Id)
        {
            Label = forecast.Label,
            Feature = forecast.Feature,
            Time = dateTime,
        });

        await _context.SaveChangesAsync();

        return true;
    }
}