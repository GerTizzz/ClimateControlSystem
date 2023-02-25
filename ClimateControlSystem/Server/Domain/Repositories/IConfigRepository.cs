using ClimateControlSystem.Server.Resources.Repository.TablesEntities;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IConfigRepository
    {
        Task<bool> UpdateConfigAsync(ConfigsEntity configToUpdate);

        Task<ConfigsEntity> GetConfigAsync();
    }
}
