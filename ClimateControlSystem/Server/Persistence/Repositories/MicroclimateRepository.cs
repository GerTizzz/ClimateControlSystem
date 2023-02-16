using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using Microsoft.EntityFrameworkCore;

namespace ClimateControlSystem.Server.Persistence.Repositories
{
    public class MicroclimateRepository : IMicroclimateRepository
    {
        private readonly PredictionsDbContext _context;

        public MicroclimateRepository(PredictionsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MonitoringsEntity>> GetMonitoringsAsync(int start, int count)
        {
            try
            {
                var monitorings = await _context.Monitorings
                    .Include(micro => micro.Prediction)
                    .Include(micro => micro.ActualData)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(start)
                    .Take(count)
                    .ToArrayAsync();

                var result = monitorings.Select(monitor =>
                    new MonitoringsEntity()
                    {
                        TracedTime = monitor.TracedTime,
                        Prediction = monitor.Prediction,
                        ActualData = monitor.ActualData
                    })
                    .ToList();

                return result;
            }
            catch (Exception exc)
            {
                return Enumerable.Empty<MonitoringsEntity>();
            }
        }

        public async Task<IEnumerable<MonitoringsEntity>> GetMonitoringsWithAccuraciesAsync(int start, int count)
        {
            try
            {
                var monitorings = await _context.Monitorings
                    .Include(micro => micro.Prediction)
                    .Include(micro => micro.ActualData)
                    .Include(micro => micro.Accuracy)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(start)
                    .Take(count)
                    .ToArrayAsync();

                var result = monitorings.Select(monitor =>
                    new MonitoringsEntity()
                    {
                        TracedTime = monitor.TracedTime,
                        Prediction = monitor.Prediction,
                        ActualData = monitor.ActualData,
                        Accuracy = monitor.Accuracy
                    })
                    .ToList();

                return result;
            }
            catch (Exception exc)
            {
                return Enumerable.Empty<MonitoringsEntity>();
            }
        }

        public async Task<IEnumerable<MonitoringsEntity>> GetMicroclimatesAsync(int start, int count)
        {
            try
            {
                var microclimates = await _context.Monitorings
                    .Include(micro => micro.Prediction)
                        .ThenInclude(prediction => prediction.Features)
                    .Include(micro => micro.Accuracy)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(start)
                    .Take(count)
                    .ToArrayAsync();

                var result = microclimates.Select(micro => 
                    new MonitoringsEntity()
                    {
                        TracedTime = micro.TracedTime,
                        Prediction = micro.Prediction,
                        Accuracy = micro.Accuracy
                    })
                    .ToArray();

                return result;
            }
            catch (Exception exc)
            {
                return Enumerable.Empty<MonitoringsEntity>();
            }
        }

        public async Task<IEnumerable<MonitoringsEntity>> GetMonitoringEventsAsync(int start, int count)
        {
            try
            {
                var monitorings = await _context.Monitorings
                    .Include(micro => micro.Prediction)
                    .Include(micro => micro.MicroclimatesEvent)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(start)
                    .Take(count)
                    .ToArrayAsync();

                var result = monitorings.Select(monitor =>
                    new MonitoringsEntity()
                    {
                        TracedTime = monitor.TracedTime,
                        MicroclimatesEvent = monitor.MicroclimatesEvent
                    })
                    .ToList();

                return result;
            }
            catch (Exception exc)
            {
                return Enumerable.Empty<MonitoringsEntity>();
            }
        }

        public async Task<int> GetMicroclimatesCountAsync()
        {
            return await _context.Monitorings.CountAsync();
        }

        public async Task<int> GetMonitoringsCountAsync()
        {
            return await _context.Predictions.CountAsync();
        }

        public async Task<ActualDataEntity?> TryGetLastActualDataAsync()
        {
            try
            {
                var actualData = (await _context.Monitorings
                    .OrderBy(record => record.Id)
                    .LastAsync()).ActualData;

                return actualData;
            }
            catch (Exception exc)
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
                    _context.Monitorings.Add(new MonitoringsEntity()
                    {
                        ActualData = monitoring.ActualData,
                        TracedTime = monitoring.TracedTime
                    });
                }
                else
                {
                    var lastMonitoring = await _context.Monitorings
                        .OrderBy(monit => monit.Id)
                        .LastAsync();

                    lastMonitoring.ActualData = monitoring.ActualData;
                    lastMonitoring.TracedTime = monitoring.TracedTime;
                }

                monitoring.ActualData = null;
                monitoring.TracedTime = null;

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
