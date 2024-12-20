using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TryClerkBlazor;
using TryClerkBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IClerkAuthService, ClerkAuthService>();

var app = builder.Build();

// Initialize Clerk for authentication (required for Clerk components)
var authService = app.Services.GetRequiredService<IClerkAuthService>();
await authService.InitializeAsync();

await app.RunAsync();
