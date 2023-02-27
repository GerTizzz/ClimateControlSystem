using Blazored.LocalStorage;
using ClimateControlSystem.Client;
using ClimateControlSystem.Client.Authentication;
using ClimateControlSystem.Client.Services.AuthenticationService;
using ClimateControlSystem.Client.Services.ConfigService;
using ClimateControlSystem.Client.Services.MonitoringService;
using ClimateControlSystem.Client.Services.UsersService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IMonitoringService, MonitoringService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IConfigService, ConfigService>();

await builder.Build().RunAsync();
