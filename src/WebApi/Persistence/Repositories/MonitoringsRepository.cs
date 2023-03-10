using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Repositories;
using WebApi.Persistence.Context;
using WebApi.Resources.Infrastructure;
using WebApi.Resources.Repository.TablesEntities;

namespace WebApi.Persistence.Repositories
{
    public class MonitoringsRepository : IMonitoringsRepository
    {
        private readonly PredictionsDbContext _context;

        public MonitoringsRepository(PredictionsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MonitoringsEntity>> GetBaseMonitoringsAsync(RequestLimits requestLimits)
        {
            try
            {
                var monitorings = _context.Monitorings
                    .Include(micro => micro.Prediction)
                    .Include(micro => micro.ActualData)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(requestLimits.Start)
                    .Take(requestLimits.Count);

                var result = await monitorings.Select(monitor =>
                    new MonitoringsEntity()
                    {
                        TracedTime = monitor.TracedTime,
                        Prediction = monitor.Prediction,
                        ActualData = monitor.ActualData
                    })
                    .ToListAsync();

                return result;
            }
            catch
            {
                return Enumerable.Empty<MonitoringsEntity>();
            }
        }

        public async Task<IEnumerable<MonitoringsEntity>> GetMonitoringsWithAccuraciesAsync(RequestLimits requestLimits)
        {
            try
            {
                var monitorings = _context.Monitorings
                    .Include(micro => micro.Prediction)
                    .Include(micro => micro.ActualData)
                    .Include(micro => micro.Accuracy)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(requestLimits.Start)
                    .Take(requestLimits.Count);

                var result = await monitorings.Select(monitor =>
                    new MonitoringsEntity()
                    {
                        TracedTime = monitor.TracedTime,
                        Prediction = monitor.Prediction,
                        ActualData = monitor.ActualData,
                        Accuracy = monitor.Accuracy
                    })
                    .ToListAsync();

                return result;
            }
            catch
            {
                return Enumerable.Empty<MonitoringsEntity>();
            }
        }

        public async Task<IEnumerable<MonitoringsEntity>> GetMicroclimatesAsync(RequestLimits requestLimits)
        {
            try
            {
                var microclimates = _context.Monitorings
                    .Include(micro => micro.Prediction)
                        .ThenInclude(prediction => prediction.Features)
                    .Include(micro => micro.Accuracy)
                    .Include(micro => micro.ActualData)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(requestLimits.Start)
                    .Take(requestLimits.Count);

                var result = await microclimates.Select(micro => 
                    new MonitoringsEntity()
                    {
                        TracedTime = micro.TracedTime,
                        Prediction = micro.Prediction,
                        Accuracy = micro.Accuracy,
                        ActualData = micro.ActualData
                    })
                    .ToListAsync();

                return result;
            }
            catch
            {
                return Enumerable.Empty<MonitoringsEntity>();
            }
        }

        public async Task<IEnumerable<MonitoringsEntity>> GetMicroclimatesEventsAsync(RequestLimits requestLimits)
        {
            try
            {
                var monitorings = _context.Monitorings
                    .Include(monitoring => monitoring.MicroclimatesEvent)
                    .Where(monitoring => monitoring.MicroclimatesEvent != null)
                    .OrderByDescending(monitoring => monitoring.Id)
                    .Skip(requestLimits.Start)
                    .Take(requestLimits.Count);

                var result = await monitorings.Select(monitoring =>
                    new MonitoringsEntity()
                    {
                        TracedTime = monitoring.TracedTime,
                        MicroclimatesEvent = monitoring.MicroclimatesEvent
                    })
                    .ToListAsync();

                return result;
            }
            catch
            {
                return Enumerable.Empty<MonitoringsEntity>();
            }
        }

        public async Task<long> GetMonitoringsCountAsync()
        {
            return await _context.Monitorings.LongCountAsync();
        }

        public async Task<long> GetMicroclimatesEventsCountAsync()
        {
            return await _context.Monitorings
                .Include(monitoring => monitoring.MicroclimatesEvent)
                .Where(monitoring => monitoring.MicroclimatesEvent != null)
                .LongCountAsync();
        }

        public async Task<PredictionsEntity?> TryGetLastPredictionAsync()
        {
            try
            {
                var lastMonitoring = await _context.Monitorings
                    .OrderByDescending(record => record.Id)
                    .FirstOrDefaultAsync();

                var lastPredictionId = lastMonitoring?.PredictionId;

                if (lastPredictionId is null)
                {
                    return null;
                }

                return await _context.Predictions.FirstOrDefaultAsync(prediction => prediction.Id == lastPredictionId);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> SaveMonitoringAsync(MonitoringsEntity monitoring)
        {
            try
            {
                if (await _context.Monitorings.AnyAsync() is false)
                {
                    await _context.Monitorings.AddAsync(new MonitoringsEntity()
                    {
                        ActualData = monitoring.ActualData,
                        TracedTime = monitoring.TracedTime
                    });

                    await _context.SaveChangesAsync();
                }
                else
                {
                    var lastMonitoring = await _context.Monitorings
                        .OrderBy(monit => monit.Id)
                        .LastAsync();

                    lastMonitoring.ActualData = monitoring.ActualData?.Clone();
                    lastMonitoring.TracedTime = monitoring.TracedTime;
                    lastMonitoring.Accuracy = monitoring.Accuracy?.Clone();
                    lastMonitoring.MicroclimatesEvent = monitoring.MicroclimatesEvent?.Clone();

                    await _context.SaveChangesAsync();
                }

                monitoring.ActualData = null;
                monitoring.TracedTime = null;
                monitoring.Accuracy = null;
                monitoring.MicroclimatesEvent = null;

                await _context.Monitorings.AddAsync(monitoring);

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
