using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IConfigRepository
    {
        Task<bool> UpdateConfig(Config configToUpdate);

        Task<Config> GetConfig();
    }
}
