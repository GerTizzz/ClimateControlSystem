using Application;
using Application.gRPC;
using Infrastructure;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.ResponseCompression;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

string modelLocation = string.Join("\\", 
    Directory.GetCurrentDirectory()
    .Split('\\')
    .TakeWhile(str => str != "src")) + "\\" + builder.Configuration["ModelLocationPath"];

string secretToken = builder.Configuration.GetSection("AppSettings:Token").Value;

builder.Services
    .AddApplication(modelLocation, secretToken)
    .AddInfrastructure()
    .AddPresentation();

builder.Services.AddRazorPages();

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

app.UseRouting();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCompression();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapGrpcService<ClimateDataReciever>();

app.MapHub<ForecastHub>("/forecasthub");
app.MapFallbackToFile("index.html");

app.Run();
