using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddDbContext<ForecastDbContext>();

            services.AddScoped<IForecastRepository, ForecastRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IConfigRepository, ConfigRepository>();

            return services;
        }
    }
}
