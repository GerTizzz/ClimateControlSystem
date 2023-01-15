using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.RepositoryResources;
using ClimateControlSystem.Shared;
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

        public async Task<MonitoringResponse[]> GetMonitorings(int start, int count)
        {
            try
            {
                var result = from microclimate in _context.Microclimates
                             join sensorData in _context.SensorsData on microclimate.SensorDataId equals sensorData.Id
                             join prediction in _context.Predictions on microclimate.PredictionId equals prediction.Id
                             join accuracy in _context.Accuracies on microclimate.AccuracyId equals accuracy.Id
                             into mixedTable
                             from accuracy in mixedTable.DefaultIfEmpty()
                             orderby microclimate.Id descending
                             select new MonitoringResponse()
                             {
                                 MeasurementTime = sensorData.MeasurementTime,
                                 PredictedFutureTemperature = prediction.PredictedTemperature,
                                 PredictedFutureHumidity = prediction.PredictedHumidity,
                                 CurrentRealTemperature = sensorData.CurrentRealTemperature,
                                 CurrentRealHumidity = sensorData.CurrentRealHumidity,
                                 PredictedTemperatureAccuracy = accuracy.PredictedTemperatureAccuracy,
                                 PredictedHumidityAccuracy = accuracy.PredictedHumidityAccuracy
                             };

                return await result.Skip(start).Take(count).ToArrayAsync();
            }
            catch (Exception exc)
            {
                return Array.Empty<MonitoringResponse>();
            }
        }

        public async Task<MicroclimateResponse[]> GetMicroclimateDataAsync(int start, int count)
        {
            try
            {
                var result = from microclimate in _context.Microclimates
                             join sensorData in _context.SensorsData on microclimate.SensorDataId equals sensorData.Id
                             join prediction in _context.Predictions on microclimate.PredictionId equals prediction.Id
                             join accuracy in _context.Accuracies on microclimate.AccuracyId equals accuracy.Id
                             into mixedTable
                             from accuracy in mixedTable.DefaultIfEmpty()
                             orderby microclimate.Id descending                        
                             select new MicroclimateResponse()
                             {
                                 MeasurementTime = sensorData.MeasurementTime,
                                 ClusterLoad = sensorData.ClusterLoad,
                                 CpuUsage = sensorData.CpuUsage,
                                 ClusterTemperature = sensorData.ClusterTemperature,
                                 CurrentRealTemperature = sensorData.CurrentRealTemperature,
                                 CurrentRealHumidity = sensorData.CurrentRealHumidity,
                                 AirHumidityOutside = sensorData.AirHumidityOutside,
                                 AirDryTemperatureOutside = sensorData.AirDryTemperatureOutside,
                                 AirWetTemperatureOutside = sensorData.AirWetTemperatureOutside,
                                 WindSpeed = sensorData.WindSpeed,
                                 WindDirection = sensorData.WindDirection,
                                 WindEnthalpy = sensorData.WindEnthalpy,
                                 MeanCoolingValue = sensorData.MeanCoolingValue,
                                 PredictedFutureTemperature = prediction.PredictedTemperature,
                                 PredictedFutureHumidity = prediction.PredictedHumidity,
                                 PredictedTemperatureAccuracy = accuracy.PredictedTemperatureAccuracy,
                                 PredictedHumidityAccuracy = accuracy.PredictedHumidityAccuracy
                             };

                return await result.Skip(start).Take(count).ToArrayAsync();
            }
            catch (Exception exc)
            {
                return Array.Empty<MicroclimateResponse>();
            }
        }

        public async Task<TemperatureEventData[]> GetTemperatureEventsAsync(int start, int count)
        {
            try
            {
                return await _context.TemperatureEvents
                    .OrderBy(tempEv => tempEv.Id)
                    .TakeLast(count)
                    .Select(tempEv => _mapper.Map<TemperatureEventData>(tempEv))
                    .ToArrayAsync();
            }
            catch (Exception exc)
            {
                return Array.Empty<TemperatureEventData>();
            }
        }

        public async Task<HumidityEventData[]> GetHumidityEventsAsync(int start, int count)
        {
            try
            {
                return await _context.HumidityEvents
                    .OrderBy(humEv => humEv.Id)
                    .TakeLast(count)
                    .Select(humEv => _mapper.Map<HumidityEventData>(humEv))
                    .ToArrayAsync();
            }
            catch (Exception exc)
            {
                return Array.Empty<HumidityEventData>();
            }
        }

        public async Task<int> GetMicroclimatesCount()
        {
            return await _context.Microclimates.CountAsync();
        }

        public async Task<PredictionResultData> GetLastPredictionAsync()
        {
            try
            {
                PredictionRecord lastRecord = await _context.Predictions
                    .OrderBy(record => record.Id)
                    .LastAsync();

                return _mapper.Map<PredictionResultData>(lastRecord);
            }
            catch (Exception exc)
            {
                return new PredictionResultData();
            }
        }

        public async Task<bool> AddMicroclimateAsync(PredictionResultData prediction, SensorsData monitoring, TemperatureEventData temperatureEvent, HumidityEventData humidityEvent)
        {
            try
            {
                MicroclimateRecord climate = new MicroclimateRecord();

                climate.SensorData = _mapper.Map<SensorsDataRecord>(monitoring);
                climate.Prediction = _mapper.Map<PredictionRecord>(prediction);
                climate.TemperatureEvent = _mapper.Map<TemperatureEventRecord>(temperatureEvent);
                climate.HumidityEvent = _mapper.Map<HumidityEventRecord>(humidityEvent);

                await _context.Microclimates.AddAsync(climate);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddAccuracyAsync(AccuracyData accuracyData)
        {
            try
            {
                AccuracyRecord accuracyRecord = _mapper.Map<AccuracyRecord>(accuracyData);

                var lastPrediction = await _context.Microclimates
                    .OrderBy(pred => pred.Id)
                    .LastOrDefaultAsync();

                if (lastPrediction != null)
                {
                    await _context.Accuracies.AddAsync(accuracyRecord);

                    lastPrediction.Accuracy = accuracyRecord;

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
    }
}
