﻿using AutoMapper;
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
                var monitorings = await _context.Microclimates
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
                        PredictedTemperature = monitor.Prediction?.PredictedTemperature,
                        PredictedHumidity = monitor.Prediction?.PredictedHumidity,
                        MeasuredTemperature = monitor.SensorData?.CurrentRealTemperature,
                        MeasuredHumidity = monitor.SensorData?.CurrentRealHumidity
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
                var monitorings = await _context.Microclimates
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
                        PredictedTemperature = monitor.Prediction?.PredictedTemperature,
                        PredictedHumidity = monitor.Prediction?.PredictedHumidity,
                        MeasuredTemperature = monitor.SensorData?.CurrentRealTemperature,
                        MeasuredHumidity = monitor.SensorData?.CurrentRealHumidity,
                        PredictedTemperatureAccuracy = monitor.Accuracy?.PredictedTemperatureAccuracy,
                        PredictedHumidityAccuracy = monitor.Accuracy?.PredictedHumidityAccuracy
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
                var microclimates = await _context.Microclimates
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
                var monitorings = await _context.Microclimates
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
                        TemperaturePredictionEvent = monitor.TemperatureEvent,
                        HumidityPredictionEvent = monitor.HumidityEvent
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
            return await _context.Microclimates.CountAsync();
        }

        public async Task<int> GetMonitoringsCountAsync()
        {
            return await _context.Predictions.CountAsync();
        }

        public async Task<PredictionResult> GetLastPredictionAsync()
        {
            try
            {
                var id = (await _context.Microclimates
                    .OrderBy(record => record.Id)
                    .LastAsync()).PredictionId;

                PredictionRecord? lastRecord = await _context.Predictions
                    .FirstOrDefaultAsync(prediction => prediction.Id == id);

                return _mapper.Map<PredictionResult>(lastRecord);
            }
            catch (Exception exc)
            {
                return new PredictionResult();
            }
        }

        public async Task<bool> AddSensorsDataAsync(SensorsData sensorsData)
        {
            try
            {
                var record = _mapper.Map<SensorsDataRecord>(sensorsData);

                if (await _context.Microclimates.AnyAsync() is false)
                {
                    var firstRecord = new MicroclimateRecord()
                    {
                        SensorData = record
                    };

                    await AddMicroclimateRecord(firstRecord);

                    return true;
                }

                var microclimate = await _context.Microclimates
                    .OrderBy(pred => pred.Id)
                    .LastAsync();

                microclimate.SensorData = record;

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddAccuracyAsync(PredictionAccuracy accuracyData)
        {
            try
            {
                AccuracyRecord accuracyRecord = _mapper.Map<AccuracyRecord>(accuracyData);

                var microclimate = await _context.Microclimates
                    .OrderBy(pred => pred.Id)
                    .LastOrDefaultAsync();

                if (microclimate != null)
                {
                    await _context.Accuracies.AddAsync(accuracyRecord);

                    microclimate.Accuracy = accuracyRecord;

                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddPredictionAsync(PredictionResult prediction, TemperatureEvent temperatureEvent, HumidityEvent humidityEvent)
        {
            try
            {
                PredictionRecord predictionRecord = _mapper.Map<PredictionRecord>(prediction);
                TemperatureEventRecord temperatureEventRecord = _mapper.Map<TemperatureEventRecord>(temperatureEvent);
                HumidityEventRecord humidityEventRecord = _mapper.Map<HumidityEventRecord>(humidityEvent);

                MicroclimateRecord microclimateRecord = new MicroclimateRecord()
                {
                    Prediction = predictionRecord,
                    TemperatureEvent = temperatureEventRecord,
                    HumidityEvent = humidityEventRecord
                };

                await AddMicroclimateRecord(microclimateRecord);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private async Task AddMicroclimateRecord(MicroclimateRecord microclimateRecord)
        {
            await _context.Microclimates.AddAsync(microclimateRecord);

            await _context.SaveChangesAsync();
        }
    }
}
