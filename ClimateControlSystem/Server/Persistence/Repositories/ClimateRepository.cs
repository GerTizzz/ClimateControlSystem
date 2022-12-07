using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.RepositoryResources;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.Enums;
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

        public async Task<List<Prediction>> GetPredictionsWithAccuraciesAsync(int amountOfRecords)
        {
            try
            {
                var result = from prediction in _context.Climates
                             join monitoring in _context.Monitorings on prediction.MonitoringDataId equals monitoring.Id
                             join accuracy in _context.Accuracies on prediction.AccuracyId equals accuracy.Id into fdt
                             from accuracy in fdt.DefaultIfEmpty()
                             orderby monitoring.MeasurementTime descending
                             select new Prediction()
                             {
                                 MeasurementTime = monitoring.MeasurementTime,
                                 //PredictedFutureTemperature = prediction.PredictedTemperature,
                                 //PredictedFutureHumidity = prediction.PredictedHumidity,
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
                ClimateRecord lastRecord = await _context.Climates
                    .OrderBy(record => record.Id)
                    .LastAsync();

                var lastPrediction = _mapper.Map<PredictionResult>(lastRecord);

                return lastPrediction;
            }
            catch (Exception exc)
            {
                return new PredictionResult();
            }
        }

        public async Task<bool> AddPredictionAsync(PredictionResult prediction, MonitoringData monitoring, AccuracyData accuracy, ClimateEventType eventType)
        {
            try
            {
                await UpdatePredictionAccuracy(accuracy);

                var predictionRecord = _mapper.Map<ClimateRecord>(prediction);
                var monitoringRecord = _mapper.Map<MonitoringRecord>(monitoring);
                var climateEventRecord = await GetClimateEventRecordByItsType(eventType);

                predictionRecord.MonitoringData = monitoringRecord;
                predictionRecord.ClimateEvent = climateEventRecord;

                await _context.Monitorings.AddAsync(monitoringRecord);
                await _context.Climates.AddAsync(predictionRecord);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private async Task<ClimateEventRecord> GetClimateEventRecordByItsType(ClimateEventType eventType)
        {
            return await _context.ClimateEvents.OrderBy(record => record.Id).FirstOrDefaultAsync(record => record.EventType == eventType);
        }

        private async Task<bool> UpdatePredictionAccuracy(AccuracyData accuracy)
        {
            try
            {
                var accuracyRecord = _mapper.Map<AccuracyRecord>(accuracy);

                await _context.Accuracies.AddAsync(accuracyRecord);

                var lastPrediction = await _context.Climates
                    .OrderBy(pred => pred.Id)
                    .LastOrDefaultAsync();

                if (lastPrediction != null)
                {
                    lastPrediction.Accuracy = accuracyRecord;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
