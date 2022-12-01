﻿using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.RepositoryResources;
using ClimateControlSystem.Shared;
using Microsoft.EntityFrameworkCore;

namespace ClimateControlSystem.Server.Persistence.Repositories
{
    public class MonitoringDataRepository : IMonitoringDataRepository
    {
        private readonly PredictionsDbContext _context;
        private readonly IMapper _mapper;

        public MonitoringDataRepository(PredictionsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddPredictionAsync(MonitoringData newMonitoringData)
        {
            try
            {
                var monitoringRecord = _mapper.Map<MonitoringRecord>(newMonitoringData);

                await _context.Monitorings.AddAsync(monitoringRecord);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
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

        public async Task<MonitoringData> GetLastPredictionAsync()
        {
            try
            {
                MonitoringRecord lastRecord = await _context.Monitorings
                    .OrderBy(record => record.Id)
                    .LastAsync();

                var lastPrediction = _mapper.Map<MonitoringData>(lastRecord);

                return lastPrediction;
            }
            catch (Exception exc)
            {
                return new MonitoringData();
            }
        }

        public async Task<bool> UpdatePredictionAccuracies(MonitoringData updatedData)
        {
            try
            {
                var dataToUpdate = await _context.Monitorings
                    .OrderByDescending(record => record.Id)
                    .LastOrDefaultAsync(record => record.MeasurementTime == updatedData.MeasurementTime);

                if (dataToUpdate is null)
                {
                    return false;
                }

                dataToUpdate.PredictedTemperatureAccuracy = updatedData.PredictedTemperatureAccuracy;
                dataToUpdate.PredictedHumidityAccuracy = updatedData.PredictedHumidityAccuracy;

                _context.Update(dataToUpdate);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }
    }
}
