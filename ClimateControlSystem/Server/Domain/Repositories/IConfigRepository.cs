using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IConfigRepository
    {
        Task<bool> UpdateConfigAsync(Config configToUpdate);

        Task<Config> GetConfigAsync();
    }
}
