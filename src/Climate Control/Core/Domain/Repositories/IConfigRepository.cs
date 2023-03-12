using Domain.Entities;

namespace Domain.Repositories
{
    public interface IConfigRepository
    {
        Task<bool> UpdateConfigAsync(Config configToUpdate);

        Task<Config> GetConfigAsync();
    }
}
