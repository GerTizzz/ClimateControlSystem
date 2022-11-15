using ClimateControlSystem.Server.Resources.RepositoryResources;
using Microsoft.EntityFrameworkCore;

namespace ClimateControlSystem.Server.Persistence.Context
{
    public class PredictionsDbContext : DbContext
    {
        public DbSet<MonitoringDataRecord> MonitoringData { get; set; }
        public PredictionsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonitoringDataRecord>().HasData(
                new MonitoringDataRecord()
                {
                    Id = 1,
                    MeasurementTime = DateTimeOffset.Now,
                    ClusterLoad = 50.8f,
                    CpuUsage = 5945.632f,
                    ClusterTemperature = 56f,
                    PreviousTemperature = 23.48f,
                    PreviousHumidity = 19.71f,
                    AirHumidityOutside = 91f,
                    AirDryTemperatureOutside = -3f,
                    AirWetTemperatureOutside = -3.91f,
                    WindSpeed = 3f,
                    WindDirection = 225f,
                    WindEnthalpy = -4.06f,
                    MeanCoolingValue = 17.7f,
                    PredictedTemperature = 23.32f,
                    PredictedHumidity = 18.77f
                });
        }
    }
}
