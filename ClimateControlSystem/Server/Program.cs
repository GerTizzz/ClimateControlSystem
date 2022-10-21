using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Mapping;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Persistence.Repositories;
using ClimateControlSystem.Server.Services;
using ClimateControlSystem.Server.Services.gRPC;
using ClimateControlSystem.Server.Services.MediatR;
using ClimateControlSystem.Server.Services.PredictionEngine;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string modelLocation = Directory.GetCurrentDirectory() + "\\" + builder.Configuration["ModelLocationPath"];

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddMediatR(typeof(MediatREntrypoint).Assembly);
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PredictionsDbConnection"));
});
builder.Services.AddScoped<IPredictionRepository, PredictionRepository>();
builder.Services.AddScoped<IPredictionService, PredictionService>();
builder.Services.AddGrpc();
builder.Services.AddSingleton<IPredictionEngineService>(sp => 
    new PredictionEngineService(sp.GetService<IMapper>(), modelLocation));

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseWebAssemblyDebugging();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapGrpcService<ClimateDataReciever>();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
