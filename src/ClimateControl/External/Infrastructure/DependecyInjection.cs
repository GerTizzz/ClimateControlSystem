using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var connectionString = string.Empty;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        connectionString = configuration.GetConnectionString("ForecastsDbConnection");

        services.AddDbContext<MonitoringDatabaseContext>(options =>
        {
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("WebApi"));
        });

        services.AddScoped<IForecastsRepository, ForecastsRepository>();

        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddScoped<IConfigsRepository, ConfigsRepository>();

        services.AddScoped<IWarningsRepository, WarningsRepository>();

        return services;
    }
}