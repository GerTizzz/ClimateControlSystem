using ClimateControl.Server.Resources.Repository.TablesEntities;

namespace ClimateControl.Server.Infrastructure.Repositories
{
    public interface IConfigRepository
    {
        Task<bool> UpdateConfigAsync(ConfigsEntity configToUpdate);

        Task<ConfigsEntity> GetConfigAsync();
    }
}
