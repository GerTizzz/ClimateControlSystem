using ClimateControlSystem.Server.Resources.RepositoryResources;
using ClimateControlSystem.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Hosting;

namespace ClimateControlSystem.Server.Persistence.Context
{
    public class PredictionsDbContext : DbContext
    {
        public DbSet<UserRecord> Users { get; set; }

        public DbSet<ClimateRecord> Climates { get; set; }
        public DbSet<MonitoringRecord> Monitorings { get; set; }
        public DbSet<AccuracyRecord> Accuracies { get; set; }
        public DbSet<PredictionRecord> Predictions { get; set; }
        public DbSet<EventTypeRecord> EventsTypes { get; set; }
        public DbSet<ConfigRecord> Configs { get; set; }

        public PredictionsDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserRecord initializedAdminUser = new()
            {
                Id = 1,
                Name = "admin",
                PasswordHash = new byte[] { 234, 67, 146, 201, 134, 164, 86, 202, 125, 217, 174, 99, 230, 69, 196, 32, 223, 130, 86, 2, 110, 245, 35, 7, 159, 20, 84, 62, 49, 84, 81, 28, 175, 203, 198, 202, 128, 63, 128, 15, 96, 135, 210, 4, 252, 15, 252, 17, 150, 160, 104, 243, 99, 40, 181, 210, 193, 226, 14, 26, 229, 165, 150, 197 },
                PasswordSalt = new byte[] { 29, 90, 245, 35, 83, 27, 162, 74, 226, 234, 171, 134, 93, 187, 246, 80, 193, 193, 90, 50, 37, 118, 116, 254, 107, 30, 200, 72, 10, 31, 43, 139, 58, 135, 118, 189, 5, 99, 211, 203, 0, 84, 81, 146, 28, 164, 132, 63, 61, 143, 124, 25, 66, 231, 99, 189, 203, 55, 91, 105, 23, 169, 254, 10, 20, 179, 147, 58, 198, 70, 204, 60, 221, 77, 160, 128, 50, 190, 189, 205, 83, 48, 107, 183, 51, 48, 173, 248, 28, 230, 153, 194, 13, 108, 51, 123, 87, 228, 62, 31, 167, 11, 30, 180, 130, 172, 254, 241, 22, 7, 150, 212, 195, 48, 144, 92, 52, 199, 221, 202, 91, 200, 83, 109, 66, 70, 223, 200 },
                Role = UserType.Admin
            };

            MonitoringRecord initializedMonitoring = new()
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

            ConfigRecord initializedConfig = new()
            {
                Id = 1,
                UpperTemperatureWarningLimit = 24f,
                LowerTemperatureWarningLimit = 16f,
                UpperTemperatureCriticalLimit = 25f,
                LowerTemperatureCriticalLimit = 15f,
                UpperHumidityWarningLimit = 21f,
                LowerHumidityWarningLimit = 10f,
                UpperHumidityCriticalLimit = 22f,
                LowerHumidityCriticalLimit = 9f
            };

            PredictionRecord initializedPrediction = new()
            {
                Id = 1,
                PredictedTemperature = 23.32f,
                PredictedHumidity = 18.77f
            };

            ClimateRecord initializedClimate = new()
            {
                Id = 1,
                PredictionId = initializedPrediction.Id,
                MonitoringDataId = initializedMonitoring.Id,
                AccuracyId = null,
                ConfigId = 1,
                Events = new()
            };

            List<EventTypeRecord> initializedClimateEventTypes = new()
            {
                new EventTypeRecord()
                {
                    Id = 1,
                    EventType = ClimateEventType.Normal
                },
                new EventTypeRecord()
                {
                    Id = 2,
                    EventType = ClimateEventType.PredictedTemperatureWarning
                },
                new EventTypeRecord()
                {
                    Id = 3,
                    EventType = ClimateEventType.PredictedHumidityWarning
                },
                new EventTypeRecord()
                {
                    Id = 4,
                    EventType = ClimateEventType.PredictedTemperatureCritical
                },
                new EventTypeRecord()
                {
                    Id = 5,
                    EventType = ClimateEventType.PredictedHumidityCritical
                }
            };

            modelBuilder.Entity<ClimateRecord>()
                .HasMany(c => c.Events)
                .WithMany(e => e.Climates)
                .UsingEntity<Dictionary<string, object>>(
                    "ClimateEvents",
                    r => r.HasOne<EventTypeRecord>().WithMany().HasForeignKey("EventId"),
                    l => l.HasOne<ClimateRecord>().WithMany().HasForeignKey("ClimateId"),
                    je =>
                    {
                        je.HasKey("ClimateId", "EventId");
                        je.HasData(
                            new 
                            { 
                                ClimateId = 1, EventId = 1
                            });
                    });

            modelBuilder.Entity<EventTypeRecord>()
                .Property(climateEvent => climateEvent.EventType)
                .HasConversion(new EnumToStringConverter<ClimateEventType>());

            modelBuilder.Entity<EventTypeRecord>()
                .HasData(initializedClimateEventTypes);

            modelBuilder.Entity<MonitoringRecord>()
                .HasData(initializedMonitoring);

            modelBuilder.Entity<PredictionRecord>()
                .HasData(initializedPrediction);

            modelBuilder.Entity<ConfigRecord>()
                .HasData(initializedConfig);

            modelBuilder.Entity<ClimateRecord>()
                .HasData(initializedClimate);

            modelBuilder.Entity<UserRecord>()
                .Property(user => user.Role)
                .HasConversion(new EnumToStringConverter<UserType>());

            modelBuilder.Entity<UserRecord>()
                .HasData(initializedAdminUser);
        }
    }
}
