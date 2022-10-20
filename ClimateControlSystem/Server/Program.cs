using ClimateControlSystem.Server.Data;
using ClimateControlSystem.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PredictionsDataManager>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PredictionsDbConnection"));
});
//builder.Services.AddLogging();
builder.Services.AddGrpc();
builder.Services.AddSingleton<IPredictionManager>(new PredictionManager(builder.Configuration["ModelLocationPath"]));
builder.Services.AddTransient<ILinkService, LinkService>();

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
//app.MapFallbackToFile("index.html");

app.Run();
