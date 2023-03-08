using Blazored.LocalStorage;
using ClimateControl.WebClient.Authentication;
using ClimateControl.WebClient.Services.AuthenticationService;
using ClimateControl.WebClient.Services.ConfigService;
using ClimateControl.WebClient.Services.MonitoringService;
using ClimateControl.WebClient.Services.UsersService;
using ClimateControl.WebClient;
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
