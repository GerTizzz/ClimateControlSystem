using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ConfigsRepository : IConfigsRepository
{
    private readonly MonitoringDatabaseContext _context;

    public ConfigsRepository(MonitoringDatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> UpdateConfigAsync(Config configToUpdate)
    {
        if (configToUpdate is null)
        {
            throw new ArgumentNullException(nameof(configToUpdate));
        }

        try
        {
            var existingConfig = await _context.Configs.OrderBy(config => config.Id).FirstAsync();

            existingConfig.UpperTemperatureWarningLimit = configToUpdate.UpperTemperatureWarningLimit;
            existingConfig.LowerTemperatureWarningLimit = configToUpdate.LowerTemperatureWarningLimit;

            existingConfig.PredictionTimeIntervalSeconds = configToUpdate.PredictionTimeIntervalSeconds;

            await _context.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Config> GetConfigAsync()
    {
        var configEntity = await _context.Configs.OrderBy(config => config.Id).FirstAsync();

        return configEntity;
    }
}