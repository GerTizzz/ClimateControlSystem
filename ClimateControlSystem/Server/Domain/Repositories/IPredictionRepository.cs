using ClimateControlSystem.Server.Resources;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IPredictionRepository
    {
        Task<int> AddPredictionAsync(ClimateRecord newRecord);
    }
}