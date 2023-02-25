using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Domain.Singletons;
using ClimateControlSystem.Server.Hubs;
using ClimateControlSystem.Server.Mapping;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Persistence.Repositories;
using ClimateControlSystem.Server.Services;
using ClimateControlSystem.Server.Services.gRPC;
using ClimateControlSystem.Server.Services.MediatR;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ClimateControlSystem.Server.Services.MediatR.Queries.ConfigRepository;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

var modelLocation = Directory.GetCurrentDirectory() + "\\" + builder.Configuration["ModelLocationPath"];
var predictionDbConnectionString = builder.Configuration.GetConnectionString("PredictionsDbConnection");

builder.Services.AddDbContext<PredictionsDbContext>(options =>
{
    options.UseSqlServer(predictionDbConnectionString);
});

builder.Services.AddScoped<IMicroclimateRepository, MicroclimateRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IConfigRepository, ConfigRepository>();

builder.Services.AddScoped<IMonitoringService, MonitoringService>();
builder.Services.AddScoped<IAuthenticateManager, AuthenticateManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IConfigManager, ConfigManager>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

builder.Services.AddSingleton<IConfigSingleton, ConfigSingleton>();

builder.Services.AddSingleton<IPredictionEngineService>(_ =>
    new PredictionEngineService(modelLocation));

builder.Services.AddMediatR(typeof(MediatREntrypoint).Assembly);
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddGrpc();

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(options =>
    options.MimeTypes = ResponseCompressionDefaults
    .MimeTypes
    .Concat(new[] { "application/octet-stream" })
);

builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseResponseCompression();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapGrpcService<ClimateDataReciever>();

app.MapControllers();
app.MapHub<MonitoringHub>("/monitoringhub");
app.MapFallbackToFile("index.html");

app.Run();
