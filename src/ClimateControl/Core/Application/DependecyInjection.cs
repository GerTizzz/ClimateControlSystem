using Application.Mapping;
using Application.Services;
using Application.Services.Implementations;
using Application.Services.Strategies;
using Application.Singletons;
using Domain.Services;
using Domain.Singletons;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        string modelLocation,
        string secretToken)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var assembly = typeof(DependecyInjection).Assembly;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretToken)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

        services.AddAutoMapper(typeof(AppMappingProfile));

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        services.AddScoped<IForecastsService, ForecastsService>();

        services.AddScoped<IAuthenticateManager, AuthenticateManager>();

        services.AddScoped<IConfigsManager, ConfigsManager>();

        services.AddSingleton<IConfigSingleton, ConfigSingleton>();

        services.AddSingleton<IPredictionEngine>(_ =>
            new PredictionEngine(modelLocation));

        services.AddGrpc();

        return services;
    }
}