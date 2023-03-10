global using Domain.Services;
global using Domain.Singletons;
using WebApi.Services;
using WebApi.Services.gRPC;
using WebApi.Services.MediatR;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Hubs;
using WebApi.Infrastructure.Repositories;
using WebApi.Infrastructure.Services;
using WebApi.Mapping;
using WebApi.Persistence.Context;
using WebApi.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

string modelLocation = string.Join("\\", Directory.GetCurrentDirectory()
    .Split('\\')
    .TakeWhile(str => str != "tests")) + "\\" + builder.Configuration["ModelLocationPath"];

string connectionString = builder.Configuration.GetConnectionString("MicroclimateMonitoringDbConnection");

builder.Services.AddSingleton<IConfigSingleton, ConfigSingleton>();

builder.Services.AddSingleton<IPredictionEngineService>(_ =>
    new PredictionEngineService(modelLocation));

builder.Services.AddScoped<IMonitoringsRepository, MonitoringsRepository>();
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

builder.Services.AddMediatR(typeof(MediatREntrypoint).Assembly);
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddDbContext<PredictionsDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddRazorPages();

builder.Services.AddGrpc();

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(options =>
    options.MimeTypes = ResponseCompressionDefaults
    .MimeTypes
    .Concat(new[] { "application/octet-stream" })
);

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
