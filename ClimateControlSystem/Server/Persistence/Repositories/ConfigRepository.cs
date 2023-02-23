using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.Common;
using Microsoft.EntityFrameworkCore;

namespace ClimateControlSystem.Server.Persistence.Repositories
{
    public class ConfigRepository : IConfigRepository
    {
        private readonly PredictionsDbContext _context;
        private readonly IMapper _mapper;

        public ConfigRepository(PredictionsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<Config> GetConfigAsync()
        {
            var existingConfig = await _context.Configs.OrderBy(config => config.Id).FirstAsync();

            var configToGive = _mapper.Map<Config>(existingConfig);

            return configToGive;
        }
    }
}
