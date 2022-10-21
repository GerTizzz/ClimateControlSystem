using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources;

namespace ClimateControlSystem.Server.Persistence.Repositories
{
    public class PredictionRepository : IPredictionRepository
    {
        private readonly AppDbContext _context;

        public PredictionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddPredictionAsync(ClimateRecord newRecord)
        {
            await _context.PredictionResults.AddAsync(newRecord);
            await _context.SaveChangesAsync();

            return _context.PredictionResults.Last().Id;
        }
    }
}
