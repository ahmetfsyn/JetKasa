using System.Text.Json.Serialization;
using JetKasa.Application;
using JetKasa.Infrastructure;
using JetKasa.WebAPI.Modules;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();

// Minimal API
app.RegisterRoutes();

// OpenAPI / Scalar UI
app.MapOpenApi();
app.MapScalarApiReference();
app.MapGet("/", () => Results.Redirect("/scalar/v1"));

app.Run();
