using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<ForecastDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IMonitoringsRepository, MonitoringsRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IConfigRepository, ConfigRepository>();

            return services;
        }
    }
}
