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
                    .Include(micro => micro.SensorsData)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(start)
                    .Take(count)
                    .ToArrayAsync();

                var result = monitorings.Select(monitor =>
                    new MonitoringsEntity()
                    {
                        MeasurementTime = monitor.MeasurementTime,
                        Prediction = monitor.Prediction,
                        //Measured
                        SensorsData = monitor.SensorsData
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
                    .Include(micro => micro.SensorsData)
                    .Include(micro => micro.Accuracy)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(start)
                    .Take(count)
                    .ToArrayAsync();

                var result = monitorings.Select(monitor =>
                    new MonitoringsEntity()
                    {
                        MeasurementTime = monitor.MeasurementTime,
                        Prediction = monitor.Prediction,
                        //Measured
                        SensorsData = monitor.SensorsData,
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
                    .Include(micro => micro.SensorsData)
                    .Include(micro => micro.Prediction)
                    .Include(micro => micro.Accuracy)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(start)
                    .Take(count)
                    .ToArrayAsync();

                var result = microclimates.Select(micro => 
                    new MonitoringsEntity()
                    {
                        MeasurementTime = micro.MeasurementTime,
                        SensorsData = micro.SensorsData,
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
                        MeasurementTime = monitor.MeasurementTime,
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

        public async Task<PredictionsEntity?> GetLastPredictionAsync()
        {
            try
            {
                var id = (await _context.Monitorings
                    .OrderBy(record => record.Id)
                    .LastAsync()).PredictionsId;

                PredictionsEntity? lastRecord = await _context.Predictions
                    .FirstOrDefaultAsync(prediction => prediction.Id == id);

                return lastRecord;
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
                await _context.Monitorings.AddAsync(monitoring);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }    
        
        public async Task<bool> SaveSensorsDataAsync(SensorsDataEntity sensorsData)
        {
            try
            {
                if (await _context.Monitorings.AnyAsync())
                {
                    var monitoringRecord = new MonitoringsEntity()
                    {
                        SensorsData = sensorsData
                    };

                    await _context.Monitorings.AddAsync(monitoringRecord);

                    await _context.SaveChangesAsync();

                    return true;
                }

                var lastMonitoringRecord = await _context.Monitorings.LastAsync();

                lastMonitoringRecord.SensorsData = sensorsData;

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
