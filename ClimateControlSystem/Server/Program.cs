using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Hubs;
using ClimateControlSystem.Server.Mapping;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Persistence.Repositories;
using ClimateControlSystem.Server.Services;
using ClimateControlSystem.Server.Services.gRPC;
using ClimateControlSystem.Server.Services.MediatR;
using ClimateControlSystem.Server.Services.PredictionEngine;
using MediatR;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//C:\Users\miste\Desktop\DC Climate Control System\ClimateControlSystem\ClimateControlSystem\Server\Resources\PredictionEngineResources\MlModel\keras model\
string _modelLocation = Directory.GetCurrentDirectory() + "\\" + builder.Configuration["ModelLocationPath"];
string _predictionDbConnectionString = builder.Configuration.GetConnectionString("PredictionsDbConnection");

//builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddMediatR(typeof(MediatREntrypoint).Assembly);
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddDbContext<PredictionsDbContext>(options =>
{
    options.UseSqlServer(_predictionDbConnectionString);
});
builder.Services.AddScoped<IPredictionRepository, PredictionRepository>();
builder.Services.AddScoped<IPredictionService, PredictionService>();
builder.Services.AddGrpc();
builder.Services.AddSingleton<IPredictionEngineService>(sp => 
    new PredictionEngineService(sp.GetService<IMapper>(), _modelLocation));
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(options =>
    options.MimeTypes = ResponseCompressionDefaults
    .MimeTypes
    .Concat(new[] { "application/octet-stream" })
);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseResponseCompression();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapGrpcService<ClimateDataReciever>();

//app.MapRazorPages();
app.MapControllers();
app.MapHub<MonitoringHub>("/monitoringhub");
app.MapFallbackToFile("index.html");

app.Run();
