using Domain.Entities;

namespace Domain.Repositories;

public interface IConfigsRepository
{
    Task<bool> UpdateConfigAsync(Config configToUpdate);

    Task<Config> GetConfigAsync();
}