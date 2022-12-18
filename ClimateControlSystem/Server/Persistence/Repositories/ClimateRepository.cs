using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.RepositoryResources;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.Common;
using ClimateControlSystem.Shared.Enums;
using ClimateControlSystem.Shared.SendToClient;
using Microsoft.EntityFrameworkCore;

namespace ClimateControlSystem.Server.Persistence.Repositories
{
    public class ClimateRepository : IClimateRepository
    {
        private readonly PredictionsDbContext _context;
        private readonly IMapper _mapper;

        public ClimateRepository(PredictionsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Prediction>> GetPredictionsWithAccuraciesAsync(int amountOfRecords)
        {
            try
            {
                var result = from climate in _context.Climates
                             join monitoring in _context.Monitorings on climate.MonitoringDataId equals monitoring.Id
                             join accuracy in _context.Accuracies on climate.AccuracyId equals accuracy.Id
                             join prediction in _context.Predictions on climate.PredictionId equals prediction.Id
                             into fdt
                             from prediction in fdt.DefaultIfEmpty()
                             orderby monitoring.MeasurementTime descending
                             select new Prediction()
                             {
                                 MeasurementTime = monitoring.MeasurementTime,
                                 PredictedFutureTemperature = prediction.PredictedTemperature,
                                 PredictedFutureHumidity = prediction.PredictedHumidity,
                                 CurrentRealTemperature = monitoring.CurrentRealTemperature,
                                 CurrentRealHumidity = monitoring.CurrentRealHumidity,
                                 PredictedTemperatureAccuracy = accuracy.PredictedTemperatureAccuracy,
                                 PredictedHumidityAccuracy = accuracy.PredictedHumidityAccuracy
                             };

                return await result.Take(amountOfRecords).ToListAsync();
            }
            catch (Exception exc)
            {
                return new List<Prediction>();
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

        public async Task<bool> AddClimateAsync(PredictionResult prediction, MonitoringData monitoring, IEnumerable<ClimateEventType> eventTypes, Config config)
        {
            try
            {
                ClimateRecord climate = new ClimateRecord();
                climate.MonitoringData = _mapper.Map<MonitoringRecord>(monitoring);
                climate.Prediction = _mapper.Map<PredictionRecord>(prediction);

                ConfigRecord configToSave = await TryFindExistingConfig(config);

                if (configToSave != null)
                {
                    climate.ConfigId = configToSave.Id;
                }
                else
                {
                    climate.Config = _mapper.Map<ConfigRecord>(config);
                }
                
                climate.Events = (await GetClimateEventRecordByItsType(eventTypes)).ToList();

                await _context.Climates.AddAsync(climate);
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

                var lastPrediction = await _context.Climates
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

        public async Task<IEnumerable<ClimateData>> GetClimateData(int amountOfRecords)
        {
            try
            {
                var result = from climate in _context.Climates
                             join monitoring in _context.Monitorings on climate.MonitoringDataId equals monitoring.Id
                             join accuracy in _context.Accuracies on climate.AccuracyId equals accuracy.Id
                             join prediction in _context.Predictions on climate.PredictionId equals prediction.Id
                             into fdt
                             from prediction in fdt.DefaultIfEmpty()
                             orderby monitoring.MeasurementTime descending
                             select new ClimateData()
                             {
                                 MeasurementTime = monitoring.MeasurementTime,
                                 ClusterLoad = monitoring.ClusterLoad,
                                 CpuUsage = monitoring.CpuUsage,
                                 ClusterTemperature = monitoring.ClusterTemperature,
                                 CurrentRealTemperature = monitoring.CurrentRealTemperature,
                                 CurrentRealHumidity = monitoring.CurrentRealHumidity,
                                 AirHumidityOutside = monitoring.AirHumidityOutside,
                                 AirDryTemperatureOutside = monitoring.AirDryTemperatureOutside,
                                 AirWetTemperatureOutside = monitoring.AirWetTemperatureOutside,
                                 WindSpeed = monitoring.WindSpeed,
                                 WindDirection = monitoring.WindDirection,
                                 WindEnthalpy = monitoring.WindEnthalpy,
                                 MeanCoolingValue = monitoring.MeanCoolingValue,
                                 PredictedFutureTemperature = prediction.PredictedTemperature,
                                 PredictedFutureHumidity = prediction.PredictedHumidity,
                                 PredictedTemperatureAccuracy = accuracy.PredictedTemperatureAccuracy,
                                 PredictedHumidityAccuracy = accuracy.PredictedHumidityAccuracy
                             };

                return await result.Take(amountOfRecords).ToListAsync();
            }
            catch (Exception exc)
            {
                return new List<ClimateData>();
            }
        }

        private async Task<ConfigRecord?> TryFindExistingConfig(Config config)
        {
            return await _context.Configs.FirstOrDefaultAsync(conf =>
            conf.UpperHumidityWarningLimit == config.UpperHumidityWarningLimit &&
            conf.UpperTemperatureWarningLimit == config.UpperTemperatureWarningLimit &&
            conf.LowerHumidityWarningLimit == config.LowerHumidityWarningLimit &&
            conf.LowerTemperatureWarningLimit == config.LowerTemperatureWarningLimit &&
            
            conf.UpperHumidityCriticalLimit == config.UpperHumidityCriticalLimit &&
            conf.UpperTemperatureCriticalLimit == config.UpperTemperatureCriticalLimit &&
            conf.LowerHumidityCriticalLimit == config.LowerHumidityCriticalLimit &&
            conf.LowerTemperatureCriticalLimit == config.LowerTemperatureCriticalLimit);
        }

        private async Task<IEnumerable<EventTypeRecord>> GetClimateEventRecordByItsType(IEnumerable<ClimateEventType> eventTypes)
        {
            var climateEvents = _context.EventsTypes.OrderBy(record => record.Id);

            List<EventTypeRecord> result = new List<EventTypeRecord>();

            foreach (var eventType in eventTypes)
            {
                result.Add(await climateEvents.FirstAsync(record => record.EventType == eventType));
            }

            return result;
        }
    }
}
