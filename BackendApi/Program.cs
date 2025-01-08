using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

var issuer = builder.Configuration["Jwt:Issuer"];
if (string.IsNullOrWhiteSpace(issuer))
    throw new InvalidOperationException("MISSING CONFIG: 'Jwt:Issuer' is required");
var authorizedParty = builder.Configuration["Jwt:AuthorizedParty"];
if (string.IsNullOrWhiteSpace(authorizedParty))
    throw new InvalidOperationException("MISSING CONFIG: 'Jwt:AuthorizedParty' is required");

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
})
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // Validate the token issuer
    options.TokenValidationParameters.ValidIssuer = issuer;
    options.Authority = issuer;
    options.ClaimsIssuer = issuer;
    // The JWT has azp but not aud, so we disable audience validation
    options.TokenValidationParameters.ValidateAudience = false;
    // And instead validate the Authorized Party (AZP) claim
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            // var aud = context.Principal?.FindFirstValue("aud");
            // if (!string.IsNullOrEmpty(aud))
            //     LogWarning("AUD Claim is present! Code must be updated to enable audience validation.");
            var azp = context.Principal?.FindFirstValue("azp");
            if (string.IsNullOrEmpty(azp))
                context.Fail("AZP Claim is missing");
            if (azp != authorizedParty)
                context.Fail("AZP Claim does not match the configured authorized party");
            return Task.CompletedTask;
        }
    };
    options.Validate();
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add CORS policy
builder.Services.AddCors(options =>
{
    // Note: This is a simple CORS policy that allows any origin, method, or header.
    // If you are building a web API that needs to be consumed by a web application,
    // you should be more restrictive.
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Use CORS middleware
app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
