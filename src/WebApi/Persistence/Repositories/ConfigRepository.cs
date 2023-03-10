using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Repositories;
using WebApi.Persistence.Context;
using WebApi.Resources.Repository.TablesEntities;

namespace WebApi.Persistence.Repositories
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
