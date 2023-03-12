using Domain.Entities;
using Domain.Primitives;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MonitoringsRepository : IMonitoringsRepository
    {
        private readonly ForecastDbContext _context;

        public MonitoringsRepository(ForecastDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Forecast>> GetForecastsAsync(IDbRequest requestLimits)
        {
            try
            {
                var forecasts = await _context.Forecasts
                    .Include(micro => micro.Label)
                    .Include(micro => micro.Fact)
                    .OrderByDescending(micro => micro.Id)
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

        public async Task<bool> UpdateForecastAsync(Forecast forecast)
        {
            if (await _context.Forecasts.AnyAsync() is false)
            {
                return false;
            }

            var lastForecast = await _context.Forecasts
                .OrderBy(forec => forec.Id)
                .LastAsync();

            lastForecast.Fact = forecast.Fact;
            lastForecast.Time = forecast.Time;
            lastForecast.Error = forecast.Error;
            lastForecast.Warning = forecast.Warning;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SaveForecastAsync(Forecast forecast)
        {
            try
            {
                await _context.Forecasts.AddAsync(forecast);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
