using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using Microsoft.EntityFrameworkCore;

namespace ClimateControlSystem.Server.Persistence.Repositories
{
    public class ConfigRepository : IConfigRepository
    {
        private readonly PredictionsDbContext _context;

        public ConfigRepository(PredictionsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateConfigAsync(ConfigsEntity configToUpdate)
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

                existingConfig.UpperHumidityWarningLimit = configToUpdate.UpperHumidityWarningLimit;
                existingConfig.LowerHumidityWarningLimit = configToUpdate.LowerHumidityWarningLimit;

                existingConfig.PredictionTimeIntervalSeconds = configToUpdate.PredictionTimeIntervalSeconds;

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ConfigsEntity> GetConfigAsync()
        {
            var configEntity = await _context.Configs.OrderBy(config => config.Id).FirstAsync();
            
            return configEntity;
        }
    }
}
