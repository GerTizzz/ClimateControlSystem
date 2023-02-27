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

var builder = WebApplication.CreateBuilder(args);

//C:\Users\miste\Desktop\DC Climate Control System\ClimateControlSystem\ClimateControlSystem\Server\Resources\PredictionEngineResources\MlModel\keras model\
string _modelLocation = Directory.GetCurrentDirectory() + "\\" + builder.Configuration["ModelLocationPath"];
string _predictionDbConnectionString = builder.Configuration.GetConnectionString("PredictionsDbConnection");

builder.Services.AddSingleton<IConfigSingleton, ConfigSingleton>();

builder.Services.AddSingleton<IPredictionEngineService>(sp =>
    new PredictionEngineService(_modelLocation));

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
    options.UseSqlServer(_predictionDbConnectionString);
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
