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
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddDbContext<ForecastDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IForecastRepository, ForecastRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IConfigRepository, ConfigRepository>();

            return services;
        }
    }
}
