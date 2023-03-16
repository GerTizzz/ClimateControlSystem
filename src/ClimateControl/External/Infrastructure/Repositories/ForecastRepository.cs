using Domain.Entities;
using Domain.Primitives;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ForecastRepository : IForecastRepository
    {
        private readonly ForecastDbContext _context;

        public ForecastRepository(ForecastDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Forecast>> GetForecastsAsync(IDbRequest requestLimits)
        {
            try
            {
                var forecasts = await _context.Forecasts
                    .Include(forecast => forecast.Label)
                    .Include(forecast => forecast.Fact)
                    .Include(forecast => forecast.Warning)
                    .Include(forecast => forecast.Error)
                    .Include(forecast => forecast.Feature)
                    .OrderByDescending(forecast => forecast.Id)
                    .Skip(requestLimits.Start)
                    .Take(requestLimits.Count)
                    .ToListAsync();

                return forecasts;
            }
            catch
            {
                return Enumerable.Empty<Forecast>();
            }
        }

        public async Task<long> GetWarningsCountAsync()
        {
            return await _context.Forecasts.LongCountAsync();
        }

        public async Task<long> GetMicroclimatesEventsCountAsync()
        {
            return await _context.Forecasts
                .Include(monitoring => monitoring.Warning)
                .Where(monitoring => monitoring.Warning != null)
                .LongCountAsync();
        }

        public async Task<Label?> TryGetLastPredictionAsync()
        {
            try
            {
                var lastMonitoring = await _context.Forecasts
                    .OrderByDescending(record => record.Id)
                    .FirstOrDefaultAsync();

                var lastPredictionId = lastMonitoring?.LabelId;

                if (lastPredictionId is null)
                {
                    return null;
                }

                return await _context.Labels.FirstOrDefaultAsync(prediction => prediction.Id == lastPredictionId);
            }
            catch
            {
                return null;
            }
        }

        //public async Task<bool> SaveForecastAsync(Forecast forecast)
        //{
        //    try
        //    {
        //        var forecastCopy = forecast;

        //        await UpdateForecastAsync(forecastCopy);

        //        forecastCopy.Fact = null;
        //        forecastCopy.Error = null;
        //        forecastCopy.Time = null;
        //        forecastCopy.Warning = null;

        //        await _context.Forecasts.AddAsync(forecastCopy);

        //        await _context.SaveChangesAsync();

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //private async Task<bool> UpdateForecastAsync(Forecast forecast)
        //{
        //    if (await _context.Forecasts.AnyAsync() is false)
        //    {
        //        return false;
        //    }

        //    var lastForecast = await _context.Forecasts
        //        .FirstOrDefaultAsync(forec => forec.Time == null);

        //    if (lastForecast is null)
        //    {
        //        return false;
        //    }

        //    lastForecast.Fact = forecast.Fact;
        //    lastForecast.Time = forecast.Time;
        //    lastForecast.Error = forecast.Error;
        //    lastForecast.Warning = forecast.Warning;

        //    await _context.SaveChangesAsync();

        //    return true;
        //}

        public async Task<bool> SaveForecastAsync(Forecast forecast)
        {

                if (await _context.Forecasts.AnyAsync() is false)
                {
                    _context.Forecasts.Add(new Forecast(Guid.NewGuid())
                    {
                        Fact = forecast.Fact?.Clone(),
                        Time = forecast.Time
                    });

                    await _context.SaveChangesAsync();
                }
                else
                {
                    var lastForecasting = await _context.Forecasts
                        .FirstOrDefaultAsync(monit => monit.Time == null);
                    
                    if (lastForecasting is not null)
                    {                        
                        if (forecast.Error is not null)
                        {
                            await _context.Errors.AddAsync(forecast.Error);
                            await _context.SaveChangesAsync();
                        }

                        if (forecast.Fact is not null)
                        {
                            await _context.Facts.AddAsync(forecast.Fact);
                            await _context.SaveChangesAsync();
                        }

                        if (forecast.Warning is not null)
                        {
                            await _context.Warnings.AddAsync(forecast.Warning);
                            await _context.SaveChangesAsync();
                        }

                        lastForecasting.Time = forecast.Time;
                        lastForecasting.Error = forecast.Error;
                        lastForecasting.Fact = forecast.Fact;
                        lastForecasting.Warning = forecast.Warning;

                        await _context.SaveChangesAsync();
                    }
                }

                await _context.Forecasts.AddAsync(new Forecast(forecast.Id)
                {
                    Label = forecast.Label?.Clone(),
                    Feature = forecast.Feature?.Clone(),
                });

                await _context.SaveChangesAsync();

                return true;

        }
    }
}
