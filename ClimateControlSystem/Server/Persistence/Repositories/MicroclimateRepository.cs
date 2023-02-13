using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.RepositoryResources;
using ClimateControlSystem.Shared.SendToClient;
using Microsoft.EntityFrameworkCore;

namespace ClimateControlSystem.Server.Persistence.Repositories
{
    public class MicroclimateRepository : IMicroclimateRepository
    {
        private readonly PredictionsDbContext _context;
        private readonly IMapper _mapper;

        public MicroclimateRepository(PredictionsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Monitoring>> GetMonitoringsAsync(int start, int count)
        {
            try
            {
                var monitorings = await _context.Monitorings
                    .Include(micro => micro.Prediction)
                    .Include(micro => micro.SensorData)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(start)
                    .Take(count)
                    .ToArrayAsync();

                var result = monitorings.Select(monitor =>
                    new Monitoring()
                    {
                        MeasurementTime = monitor.SensorData?.MeasurementTime,
                        Prediction = _mapper.Map<Prediction>(monitor.Prediction),
                        MeasuredData = _mapper.Map<MeasuredData>(monitor.SensorData)
                    })
                    .ToList();

                return result;
            }
            catch (Exception exc)
            {
                return Enumerable.Empty<Monitoring>();
            }
        }

        public async Task<IEnumerable<Monitoring>> GetMonitoringsWithAccuraciesAsync(int start, int count)
        {
            try
            {
                var monitorings = await _context.Monitorings
                    .Include(micro => micro.Prediction)
                    .Include(micro => micro.SensorData)
                    .Include(micro => micro.Accuracy)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(start)
                    .Take(count)
                    .ToArrayAsync();

                var result = monitorings.Select(monitor =>
                    new Monitoring()
                    {
                        MeasurementTime = monitor.SensorData?.MeasurementTime,
                        Prediction = _mapper.Map<Prediction>(monitor.Prediction),
                        MeasuredData = _mapper.Map<MeasuredData>(monitor.SensorData),
                        Accuracy = _mapper.Map<Accuracy>(monitor.Accuracy)
                    })
                    .ToList();

                return result;
            }
            catch (Exception exc)
            {
                return Enumerable.Empty<Monitoring>();
            }
        }

        public async Task<IEnumerable<MicroclimateResponse>> GetMicroclimatesAsync(int start, int count)
        {
            try
            {
                var microclimates = await _context.Monitorings
                    .Include(micro => micro.SensorData)
                    .Include(micro => micro.Prediction)
                    .Include(micro => micro.Accuracy)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(start)
                    .Take(count)
                    .ToArrayAsync();

                var result = microclimates.Select(micro => 
                    new MicroclimateResponse()
                    {
                            MeasurementTime = micro.SensorData?.MeasurementTime,
                            ClusterLoad = micro.SensorData?.ClusterLoad,
                            CpuUsage = micro.SensorData?.CpuUsage,
                            ClusterTemperature = micro.SensorData?.ClusterTemperature,
                            CurrentRealTemperature = micro.SensorData?.CurrentRealTemperature,
                            CurrentRealHumidity = micro.SensorData?.CurrentRealHumidity,
                            AirHumidityOutside = micro.SensorData?.AirHumidityOutside,
                            AirDryTemperatureOutside = micro.SensorData?.AirDryTemperatureOutside,
                            AirWetTemperatureOutside = micro.SensorData?.AirWetTemperatureOutside,
                            WindSpeed = micro.SensorData?.WindSpeed,
                            WindDirection = micro.SensorData?.WindDirection,
                            WindEnthalpy = micro.SensorData?.WindEnthalpy,
                            MeanCoolingValue = micro.SensorData?.MeanCoolingValue,
                            PredictedFutureTemperature = micro.Prediction?.PredictedTemperature,
                            PredictedFutureHumidity = micro.Prediction?.PredictedHumidity,
                            PredictedTemperatureAccuracy = micro.Accuracy?.PredictedTemperatureAccuracy,
                            PredictedHumidityAccuracy = micro.Accuracy?.PredictedHumidityAccuracy
                    })
                    .ToArray();

                return result;
            }
            catch (Exception exc)
            {
                return Enumerable.Empty<MicroclimateResponse>();
            }
        }

        public async Task<IEnumerable<Monitoring>> GetMonitoringEventsAsync(int start, int count)
        {
            try
            {
                var monitorings = await _context.Monitorings
                    .Include(micro => micro.Prediction)
                    .Include(micro => micro.SensorData)
                    .Include(micro => micro.Accuracy)
                    .OrderByDescending(micro => micro.Id)
                    .Skip(start)
                    .Take(count)
                    .ToArrayAsync();

                var result = monitorings.Select(monitor =>
                    new Monitoring()
                    {
                        MeasurementTime = monitor.SensorData?.MeasurementTime,
                        MicroclimateEvent = new MicroclimateEvent()
                        {
                            TemperatureValue = monitor.MicroclimateEvent?.TempertatureValue,
                            HumidityValue = monitor.MicroclimateEvent?.HumidityValue
                        }
                    })
                    .ToList();

                return result;
            }
            catch (Exception exc)
            {
                return Enumerable.Empty<Monitoring>();
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

        public async Task<Prediction> GetLastPredictionAsync()
        {
            try
            {
                var id = (await _context.Monitorings
                    .OrderBy(record => record.Id)
                    .LastAsync()).PredictionId;

                PredictionRecord? lastRecord = await _context.Predictions
                    .FirstOrDefaultAsync(prediction => prediction.Id == id);

                return _mapper.Map<Prediction>(lastRecord);
            }
            catch (Exception exc)
            {
                return new Prediction();
            }
        }

        public async Task<bool> SaveMonitoringAsync(Monitoring monitoring)
        {
            try
            {
                var monitoringRecord = _mapper.Map<MonitoringRecord>(monitoring);

                await _context.Monitorings.AddAsync(monitoringRecord);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }    
        
        public async Task<bool> SaveOrUpdateSensorsDataAsync(SensorsData sensorsData)
        {
            try
            {
                var monitoringRecord = _mapper.Map<MonitoringRecord>(monitoring);

                await _context.Monitorings.AddAsync(monitoringRecord);

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
