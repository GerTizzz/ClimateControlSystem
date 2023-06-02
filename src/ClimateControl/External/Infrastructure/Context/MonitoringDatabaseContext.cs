using Domain.Entities;
using Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Context;

public sealed class MonitoringDatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Config> Configs { get; set; }

    public DbSet<Forecast> Forecasts { get; set; }
    
    public DbSet<Feature> Features { get; set; }
    
    public DbSet<PredictedValue> Predictions { get; set; }
    public DbSet<Warning> Warnings { get; set; }

    public MonitoringDatabaseContext(DbContextOptions options) : base(options)
    {
        try
        {
            Database.Migrate();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var admin = new User(Guid.NewGuid(), "admin", UserType.Admin,
            new byte[] { 234, 67, 146, 201, 134, 164, 86, 202, 125, 217, 174, 99, 230, 69, 196, 32, 223, 130, 86, 2, 110, 245, 35, 7, 159, 20, 84, 62, 49, 84, 81, 28, 175, 203, 198, 202, 128, 63, 128, 15, 96, 135, 210, 4, 252, 15, 252, 17, 150, 160, 104, 243, 99, 40, 181, 210, 193, 226, 14, 26, 229, 165, 150, 197 },
            new byte[] { 29, 90, 245, 35, 83, 27, 162, 74, 226, 234, 171, 134, 93, 187, 246, 80, 193, 193, 90, 50, 37, 118, 116, 254, 107, 30, 200, 72, 10, 31, 43, 139, 58, 135, 118, 189, 5, 99, 211, 203, 0, 84, 81, 146, 28, 164, 132, 63, 61, 143, 124, 25, 66, 231, 99, 189, 203, 55, 91, 105, 23, 169, 254, 10, 20, 179, 147, 58, 198, 70, 204, 60, 221, 77, 160, 128, 50, 190, 189, 205, 83, 48, 107, 183, 51, 48, 173, 248, 28, 230, 153, 194, 13, 108, 51, 123, 87, 228, 62, 31, 167, 11, 30, 180, 130, 172, 254, 241, 22, 7, 150, 212, 195, 48, 144, 92, 52, 199, 221, 202, 91, 200, 83, 109, 66, 70, 223, 200 });

        var config = new Config(Guid.NewGuid(),
            27f,
            18f);

        var features = InitializeFeatures();

        modelBuilder.Entity<Feature>()
            .HasData(features);

        modelBuilder.Entity<Forecast>()
            .HasData(InitializeForecasts(features));

        modelBuilder.Entity<Config>()
            .HasData(config);

        modelBuilder.Entity<User>()
            .Property(user => user.Role)
            .HasConversion(new EnumToStringConverter<UserType>());
        
        modelBuilder.Entity<Warning>()
            .Property(warning => warning.Type)
            .HasConversion(new EnumToStringConverter<WarningType>());
        
        modelBuilder.Entity<Warning>()
            .HasData(
            new Warning(Guid.NewGuid(),
            "Ожидается критическое повышение температуры! Необходимо принять меры: увеличеть мощность охлаждения!",
            WarningType.CriticalUpper),
            new Warning(Guid.NewGuid(),
            "Ожидается критическое снижение температуры! Необходимо принять меры: уменьшить мощность охлаждения!",
            WarningType.CriticalLower),
            new Warning(Guid.NewGuid(),
            "Ожидается повышение температуры выше оптимальной! Для более эффективной работы необходимо уменьшить мощность охлаждения!",
            WarningType.Upper),
            new Warning(Guid.NewGuid(),
            "Ожидается понижение температуры ниже оптимальной! Для более эффективной работы необходимо увеличить мощность охлаждения!",
            WarningType.Lower));

        modelBuilder.Entity<User>()
            .HasData(admin);
    }

    private List<Forecast> InitializeForecasts(List<Feature> features)
    {
        var forecasts = new List<Forecast>();

        var currentTime = DateTimeOffset.Now;

        for (var i = 0; i < features.Count; i++)
        {
            forecasts.Add(new Forecast(Guid.NewGuid())
            {
                FeatureId = features[i].Id,
                Time = currentTime.AddHours(features.Count * -1 + i)
            });
        }

        return forecasts;
    }

    private List<Feature> InitializeFeatures()
    {
        string[] lstmInitiateData = {"-13.7;17.91;20",
"-8.4;17.61;18",
"-8.25;16.9;18",
"-8.1;17.25;18",
"-7.95;17.17;18",
"-7.8;17.61;18",
"-7.65;17.67;18",
"-7.5;17.73;19",
"-7.35;18.32;18",
"-7.2;17.33;19",
"-7.05;17.96;18",
"-6.9;17.36;18",
"-6.75;17.55;18",
"-6.6;17.74;18",
"-6.45;17.68;18",
"-6.3;17.35;19",
"-6.15;18.05;18",
"-6;17.8;18",
"-5.85;17.95;18",
"-5.7;18.11;18",
"-14;18.21;21",
"-2;18.86;18",
"-14.06;18.57;20",
"-2;17.74;17",
"-13.79;18.09;20",
"-14.11;18.02;21",
"-2;18.68;18",
"-14.17;18.37;21",
"-2;19.02;17",
"-13.88;18.1;20",
"-14.22;18.06;22",
"-2;19.37;17",
"-14.28;17.75;21",
"-2;18.85;16",
"-13.97;17.47;20",
"-13.52;18.04;19",
"-8.51;17.06;19",
"-14.33;17.77;22",
"-2;19;18",
"-5.84;18.85;19",
"-13.89;18.52;20",
"-14.39;17.76;22",
"-2.11;19.31;18",
"-2;18.72;17",
"-14.06;18;19",
"-14.44;17.32;22",
"-2;18.93;17",
"-14.5;18.02;22",
"-2;19.23;16",
"-14.15;17.62;19",
"-14.56;17.03;22",
"-2;19.25;14",
"-14.61;15.87;21",
"-2;18.07;17",
"-14.23;17.56;21",
"-13.33;18.33;19",
"-8.62;17.42;18",
"-14.67;17.21;22",
"-2;18.98;16",
"-14.72;17;22",
"-2;19.06;17",
"-14.32;17.63;21",
"-5.99;18.78;19",
"-13.78;18.46;19",
"-14.78;17.01;22",
"-2.22;19.22;19",
"-2;19.48;15",
"-14.83;16.73;22",
"-2;18.66;16",
"-14.41;16.89;19",
"-14.89;17.04;21",
"-2;18.59;15",
"-14.94;16.59;23",
"-2;19.39;16",
"-12.9;17.12;19",
"-14.5;16.83;21",
"-13.15;18.14;19",
"-8.73;17.45;17",
"-15;16.34;19",
"-14.89;16.35;18",
"-14.78;15.51;20",
"-14.67;16.73;19",
"-14.56;16.91;19",
"-14.44;17.04;19",
"-14.33;16.42;19",
"-14.22;16.3;19",
"-14.11;16.69;19",
"-14;16.75;19",
"-13.89;16.67;17",
"-13.78;15.41;19",
"-13.67;16.72;19",
"-13.56;16.72;20",
"-13.44;17.34;19",
"-13.33;16.75;19",
"-13.22;17.26;19",
"-13.11;16.67;19",
"-13;16.72;19",
"-12.89;16.61;18",
"-12.78;15.94;20",
"-12.67;17.23;18",
"-12.56;16.24;20",
"-12.44;17.66;18",
"-12.33;16.44;20",
"-12.22;17.43;19",
"-12.11;17.65;19",
"-12;17.72;19",
"-11.89;17.35;20",
"-11.78;18.01;20",
"-11.67;18.36;19",
"-11.56;17.65;19",
"-11.44;17.68;20",
"-11.33;18.08;19",
"-11.22;17.96;20",
"-11.11;18.34;20",
"-11;18.46;20",
"-10.94;18.56;19",
"-10.89;17.88;20",
"-10.83;18.11;20",
"-10.78;18.76;19",
"-10.72;18.08;20",
"-10.67;18.32;19",
"-10.61;17.84;19",
"-10.56;17.97;20",
"-10.5;18.19;20",
"-10.45;18.46;20",
"-10.39;18.66;19",
"-10.33;18.17;19",
"-10.28;18.19;20",
"-10.22;18.68;19",
"-10.17;18.09;19",
"-10.11;17.93;20",
"-10.06;18.47;20",
"-10;18.49;20",
"-9.83;18.55;20",
"-9.67;18.78;20",
"-9.5;18.59;19",
"-9.33;18.26;19",
"-9.17;17.8;20",
"-9;18.78;18",
"-8.83;17.8;19",
"-8.67;17.9;18",
"-8.5;16.93;19",
"-8.33;18.06;19",
"-8.17;18.18;18"};

        var features = new List<Feature>();

        for (var i = 0; i < lstmInitiateData.Length; i++)
        {
            var parsedFeatures = lstmInitiateData[i].Split(";").ToArray();

            features.Add(
                new Feature(
                    Guid.NewGuid(),
                    float.Parse(parsedFeatures[0].Replace('.', ',')),
                    float.Parse(parsedFeatures[1].Replace('.', ',')),
                    float.Parse(parsedFeatures[2].Replace('.', ',')))
                );
        }

        return features;
    }
}