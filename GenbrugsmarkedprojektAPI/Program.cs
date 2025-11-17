using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000); // Api kører på port 5000
});

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// CORS - Til udvikling
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Check at API kører
app.MapGet("/status", () => Results.Ok("OK"));

app.MapControllers();

// OpenAPI
app.MapOpenApi();

app.Run();