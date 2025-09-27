using System.Text.Json.Serialization;
using JetKasa.Application;
using JetKasa.Infrastructure;
using JetKasa.WebAPI.Hubs;
using JetKasa.WebAPI.Modules;
using Scalar.AspNetCore;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Services
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSignalR();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();

// Middleware
app.UseHttpsRedirection();

// Minimal API
app.RegisterRoutes();

app.UseCors("AllowFrontend");


// OpenAPI / Scalar UI
app.MapHub<PaymentHub>("/hub");
app.MapOpenApi();
app.MapScalarApiReference();
app.MapGet("/", () => Results.Redirect("/scalar/v1"));

app.Run();
