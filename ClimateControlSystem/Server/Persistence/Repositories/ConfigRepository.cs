using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.RepositoryResources;
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
            try
            {
                ConfigRecord existingConfig = await _context.Configs.OrderBy(config => config.Id).FirstAsync();

                existingConfig.UpperTemperatureWarningLimit = configToUpdate.UpperTemperatureWarningLimit;
                existingConfig.LowerTemperatureWarningLimit = configToUpdate.LowerTemperatureWarningLimit;

                existingConfig.UpperHumidityWarningLimit = configToUpdate.UpperHumidityWarningLimit;
                existingConfig.LowerHumidityWarningLimit = configToUpdate.LowerHumidityWarningLimit;

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
            try
            {
                var existingConfig = await _context.Configs.OrderBy(config => config.Id).FirstAsync();

                Config configToGive = _mapper.Map<Config>(existingConfig);

                return configToGive;
            }
            catch
            {
                return null;
            }
        }
    }
}
