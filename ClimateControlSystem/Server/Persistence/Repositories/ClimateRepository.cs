using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.RepositoryResources;
using ClimateControlSystem.Shared;
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

        public async Task<bool> AddPredictionAsync(PredictionData newMonitoringData)
        {
            try
            {
                var predictionRecord = _mapper.Map<PredictionRecord>(newMonitoringData);

                await _context.Predictions.AddAsync(predictionRecord);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddAccuracyAsync(AccuracyData accuracy)
        {
            try
            {
                var accuracyRecord = _mapper.Map<AccuracyRecord>(accuracy);

                await _context.Accuracies.AddAsync(accuracyRecord);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<MonitoringData>> GetClimateRecordsAsync(int amountOfRecords)
        {
            try
            {
                var result = await _context.Monitorings
                    .OrderByDescending(record => record.MeasurementTime)
                    .Take(amountOfRecords)
                    .Select(record => _mapper.Map<MonitoringData>(record))
                    .ToListAsync();
                return result;
            }
            catch (Exception exc)
            {
                return await Task.FromResult(new List<MonitoringData>());
            }
        }

        public async Task<PredictionData> GetLastPredictionAsync()
        {
            try
            {
                PredictionRecord lastRecord = await _context.Predictions
                    .OrderBy(record => record.Id)
                    .LastAsync();

                var lastPrediction = _mapper.Map<PredictionData>(lastRecord);

                return lastPrediction;
            }
            catch (Exception exc)
            {
                return new PredictionData();
            }
        }
    }
}
