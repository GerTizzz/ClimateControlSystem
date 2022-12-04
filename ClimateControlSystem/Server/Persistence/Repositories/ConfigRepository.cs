using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.RepositoryResources;
using ClimateControlSystem.Shared.Common;
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

        public async Task<bool> UpdateConfig(Config configToUpdate)
        {
            try
            {
                ConfigRecord existingConfig = await _context.Configs.OrderBy(config => config.Id).FirstAsync();

                existingConfig.TemperatureLimit = configToUpdate.TemperatureLimit;
                existingConfig.HumidityLimit = configToUpdate.HumidityLimit;

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Config> GetConfig()
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
