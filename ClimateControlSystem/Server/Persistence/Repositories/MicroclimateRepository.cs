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

        public async Task<MonitoringResponse[]> GetMonitoringsAsync(int start, int count)
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

        public async Task<MicroclimateResponse[]> GetMicroclimatesAsync(int start, int count)
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

        public async Task<TemperatureEvent[]> GetTemperatureEventsAsync(int start, int count)
        {
            try
            {
                return await _context.TemperatureEvents
                    .OrderBy(tempEv => tempEv.Id)
                    .TakeLast(count)
                    .Select(tempEv => _mapper.Map<TemperatureEvent>(tempEv))
                    .ToArrayAsync();
            }
            catch (Exception exc)
            {
                return Array.Empty<TemperatureEvent>();
            }
        }

        public async Task<HumidityEvent[]> GetHumidityEventsAsync(int start, int count)
        {
            try
            {
                return await _context.HumidityEvents
                    .OrderBy(humEv => humEv.Id)
                    .TakeLast(count)
                    .Select(humEv => _mapper.Map<HumidityEvent>(humEv))
                    .ToArrayAsync();
            }
            catch (Exception exc)
            {
                return Array.Empty<HumidityEvent>();
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

                var microclimate = await _context.Microclimates
                        .OrderBy(pred => pred.Id)
                        .LastOrDefaultAsync();

                if (microclimate != null)
                {
                    microclimate.SensorData = record;
                }
                else
                {
                    await _context.Microclimates.AddAsync(new MicroclimateRecord() { SensorData = record });
                }

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

                await _context.Microclimates.AddAsync(microclimateRecord);

                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
