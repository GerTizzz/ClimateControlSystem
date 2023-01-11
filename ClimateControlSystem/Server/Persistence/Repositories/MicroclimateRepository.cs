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

        public async Task<IEnumerable<Monitoring>> GetMonitorings(int amountOfRecords)
        {
            try
            {
                var result = from microclimate in _context.Microclimates
                             join sensorData in _context.SensorsData on microclimate.SensorDataId equals sensorData.Id
                             join prediction in _context.Predictions on microclimate.PredictionId equals prediction.Id
                             join accuracy in _context.Accuracies on microclimate.AccuracyId equals accuracy.Id
                             into mixedTable
                             from accuracy in mixedTable.DefaultIfEmpty()
                             orderby sensorData.MeasurementTime descending
                             select new Monitoring()
                             {
                                 MeasurementTime = sensorData.MeasurementTime,
                                 PredictedFutureTemperature = prediction.PredictedTemperature,
                                 PredictedFutureHumidity = prediction.PredictedHumidity,
                                 CurrentRealTemperature = sensorData.CurrentRealTemperature,
                                 CurrentRealHumidity = sensorData.CurrentRealHumidity,
                                 PredictedTemperatureAccuracy = accuracy.PredictedTemperatureAccuracy,
                                 PredictedHumidityAccuracy = accuracy.PredictedHumidityAccuracy
                             };

                return await result.Take(amountOfRecords).ToListAsync();
            }
            catch (Exception exc)
            {
                return new List<Monitoring>();
            }
        }

        public async Task<PredictionResult> GetLastPredictionAsync()
        {
            try
            {
                PredictionRecord lastRecord = await _context.Predictions
                    .OrderBy(record => record.Id)
                    .LastAsync();

                return _mapper.Map<PredictionResult>(lastRecord);
            }
            catch (Exception exc)
            {
                return new PredictionResult();
            }
        }

        public async Task<bool> AddMicroclimateAsync(PredictionResult prediction, SensorsData monitoring, TemperatureEvent temperatureEvent, HumidityEvent humidityEvent)
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

        public async Task<IEnumerable<MicroclimateData>> GetMicroclimateDataAsync(int amountOfRecords)
        {
            try
            {
                var result = from microclimate in _context.Microclimates
                             join sensorData in _context.SensorsData on microclimate.SensorDataId equals sensorData.Id
                             join prediction in _context.Predictions on microclimate.PredictionId equals prediction.Id
                             join accuracy in _context.Accuracies on microclimate.AccuracyId equals accuracy.Id
                             into mixedTable
                             from accuracy in mixedTable.DefaultIfEmpty()
                             orderby sensorData.MeasurementTime descending
                             select new MicroclimateData()
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

                return await result.Take(amountOfRecords).ToListAsync();
            }
            catch (Exception exc)
            {
                return new List<MicroclimateData>();
            }
        }
    }
}
