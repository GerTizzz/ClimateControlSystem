using WebApi.Resources.Repository.TablesEntities;

namespace WebApi.Infrastructure.Repositories
{
    public interface IConfigRepository
    {
        Task<bool> UpdateConfigAsync(ConfigsEntity configToUpdate);

        Task<ConfigsEntity> GetConfigAsync();
    }
}
