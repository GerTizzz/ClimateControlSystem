﻿using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Infrastructure;
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
                    .OrderByDescending(micro => micro.Id)
                    .Skip(requestLimits.Start)
                    .Take(requestLimits.Count);

                var result = await microclimates.Select(micro => 
                    new MonitoringsEntity()
                    {
                        TracedTime = micro.TracedTime,
                        Prediction = micro.Prediction,
                        Accuracy = micro.Accuracy
                    })
                    .ToListAsync();

                return result;
            }
            catch
            {
                return Enumerable.Empty<MonitoringsEntity>();
            }
        }

        public async Task<IEnumerable<MonitoringsEntity>> GetMonitoringEventsAsync(RequestLimits requestLimits)
        {
            try
            {
                var monitorings = _context.Monitorings
                    .Include(micro => micro.Prediction)
                    .Include(micro => micro.MicroclimatesEvent)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(requestLimits.Start)
                    .Take(requestLimits.Count);

                var result = await monitorings.Select(monitor =>
                    new MonitoringsEntity()
                    {
                        TracedTime = monitor.TracedTime,
                        MicroclimatesEvent = monitor.MicroclimatesEvent
                    })
                    .ToListAsync();

                return result;
            }
            catch
            {
                return Enumerable.Empty<MonitoringsEntity>();
            }
        }

        public async Task<long> GetMicroclimatesCountAsync()
        {
            return await _context.Monitorings.LongCountAsync();
        }

        public async Task<long> GetMonitoringsCountAsync()
        {
            return await _context.Predictions.LongCountAsync();
        }

        public async Task<PredictionsEntity?> TryGetLastPredictionAsync()
        {
            try
            {
                var lastMonitoring = await _context.Monitorings
                    .OrderByDescending(record => record.Id)
                    .FirstOrDefaultAsync();

                return lastMonitoring?.Prediction;
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
                    _context.Monitorings.Add(new MonitoringsEntity()
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

                    lastMonitoring.ActualData = monitoring.ActualData;
                    lastMonitoring.TracedTime = monitoring.TracedTime;
                    lastMonitoring.Accuracy = monitoring.Accuracy;
                    lastMonitoring.MicroclimatesEvent = monitoring.MicroclimatesEvent;

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
