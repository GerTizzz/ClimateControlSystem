using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            string connectionString = string.Empty;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = configuration.GetConnectionString("ForecastsDbConnection");

            services.AddDbContext<ForecastDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("WebApi"));
            });

            services.AddScoped<IForecastRepository, ForecastRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IConfigRepository, ConfigRepository>();

            return services;
        }
    }
}
