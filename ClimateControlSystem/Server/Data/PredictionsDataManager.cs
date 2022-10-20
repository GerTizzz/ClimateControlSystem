using Microsoft.EntityFrameworkCore;

namespace ClimateControlSystem.Server.Data
{
    public class PredictionsDataManager : DbContext
    {
        public PredictionsDataManager(DbContextOptions<PredictionsDataManager> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer();
        }
    }
}
