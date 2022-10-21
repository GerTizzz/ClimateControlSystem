using ClimateControlSystem.Server.Resources;
using Microsoft.EntityFrameworkCore;

namespace ClimateControlSystem.Server.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<ClimateRecord> PredictionResults { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
