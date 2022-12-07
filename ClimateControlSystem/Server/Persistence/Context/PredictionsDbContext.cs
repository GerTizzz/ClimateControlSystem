using ClimateControlSystem.Server.Resources.RepositoryResources;
using ClimateControlSystem.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClimateControlSystem.Server.Persistence.Context
{
    public class PredictionsDbContext : DbContext
    {
        public DbSet<UserRecord> Users { get; set; }

        public DbSet<ClimateRecord> Climates { get; set; }
        public DbSet<MonitoringRecord> Monitorings { get; set; }
        public DbSet<AccuracyRecord> Accuracies { get; set; }
        public DbSet<PredictionRecord> Predictions { get; set; }
        public DbSet<ClimateEventRecord> ClimateEvents { get; set; }
        public DbSet<ConfigRecord> Configs { get; set; }
        public PredictionsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var initializedMonitoring = new MonitoringRecord()
            {
                Id = 1,
                MeasurementTime = DateTimeOffset.Now,
                ClusterLoad = 50.8f,
                CpuUsage = 5945.632f,
                ClusterTemperature = 56f,
                CurrentRealTemperature = 23.48f,
                CurrentRealHumidity = 19.71f,
                AirHumidityOutside = 91f,
                AirDryTemperatureOutside = -3f,
                AirWetTemperatureOutside = -3.91f,
                WindSpeed = 3f,
                WindDirection = 225f,
                WindEnthalpy = -4.06f,
                MeanCoolingValue = 17.7f
            };

            var initializedClimateEventTypes = new List<ClimateEventRecord>()
            {
                new ClimateEventRecord()
                {
                    Id = 1,
                    EventType = ClimateEventType.Normal
                },
                new ClimateEventRecord()
                {
                    Id = 2,
                    EventType = ClimateEventType.PredictedTemperatureWarning
                },
                new ClimateEventRecord()
                {
                    Id = 3,
                    EventType = ClimateEventType.PredictedHumidityWarning
                },
                new ClimateEventRecord()
                {
                    Id = 4,
                    EventType = ClimateEventType.Critical
                }
            };

            var initializedConfig = new ConfigRecord()
            {
                Id = 1,
                TemperatureLimit = 24f,
                HumidityLimit = 21f
            };

            var initializedPrediction = new PredictionRecord()
            {
                Id = 1,
                PredictedTemperature = 23.32f,
                PredictedHumidity = 18.77f
            };

            var initializedClimate = new ClimateRecord()
            {
                Id = 1,
                PredictionId = initializedPrediction.Id,
                MonitoringDataId = initializedMonitoring.Id,
                AccuracyId = null,
                ConfigId = initializedConfig.Id,
                ClimateEventId = 1
            };

            var initializedAdminUser = new UserRecord()
            {
                Id = 1,
                Name = "admin",
                PasswordHash = new byte[] { 234, 67, 146, 201, 134, 164, 86, 202, 125, 217, 174, 99, 230, 69, 196, 32, 223, 130, 86, 2, 110, 245, 35, 7, 159, 20, 84, 62, 49, 84, 81, 28, 175, 203, 198, 202, 128, 63, 128, 15, 96, 135, 210, 4, 252, 15, 252, 17, 150, 160, 104, 243, 99, 40, 181, 210, 193, 226, 14, 26, 229, 165, 150, 197 },
                PasswordSalt = new byte[] { 29, 90, 245, 35, 83, 27, 162, 74, 226, 234, 171, 134, 93, 187, 246, 80, 193, 193, 90, 50, 37, 118, 116, 254, 107, 30, 200, 72, 10, 31, 43, 139, 58, 135, 118, 189, 5, 99, 211, 203, 0, 84, 81, 146, 28, 164, 132, 63, 61, 143, 124, 25, 66, 231, 99, 189, 203, 55, 91, 105, 23, 169, 254, 10, 20, 179, 147, 58, 198, 70, 204, 60, 221, 77, 160, 128, 50, 190, 189, 205, 83, 48, 107, 183, 51, 48, 173, 248, 28, 230, 153, 194, 13, 108, 51, 123, 87, 228, 62, 31, 167, 11, 30, 180, 130, 172, 254, 241, 22, 7, 150, 212, 195, 48, 144, 92, 52, 199, 221, 202, 91, 200, 83, 109, 66, 70, 223, 200 },
                Role = UserType.Admin
            };

            modelBuilder.Entity<ClimateEventRecord>()
                .Property(climateEvent => climateEvent.EventType)
                .HasConversion<int>();

            modelBuilder.Entity<ClimateEventRecord>().HasData(initializedClimateEventTypes);

            modelBuilder.Entity<MonitoringRecord>().HasData(initializedMonitoring);

            modelBuilder.Entity<ClimateRecord>().HasData(initializedClimate);

            modelBuilder.Entity<PredictionRecord>().HasData(initializedPrediction);

            modelBuilder.Entity<ConfigRecord>().HasData(initializedConfig);

            modelBuilder.Entity<UserRecord>()
                .Property(user => user.Role)
                .HasConversion(new EnumToStringConverter<UserType>());

            modelBuilder.Entity<UserRecord>().HasData(initializedAdminUser);
        }
    }
}
