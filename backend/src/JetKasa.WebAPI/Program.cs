using System.Text.Json.Serialization;
using JetKasa.Application;
using JetKasa.Infrastructure;
using JetKasa.WebAPI.Hubs;
using JetKasa.WebAPI.Modules;
using Scalar.AspNetCore;



var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
System.Console.WriteLine(port);
builder.WebHost.UseUrls($"http://*:{port}");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebAndMobileApps", policy =>
    {
        policy
            .SetIsOriginAllowed(_ => true) // her origin'e izin verir (sadece dev için)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // WebSocket için zorunlu
    });
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
if (!app.Environment.IsDevelopment())
{
    // System.Console.WriteLine("productta");
    // app.UseHttpsRedirection();
}

// Minimal API
app.RegisterRoutes();

app.UseCors("AllowWebAndMobileApps");


// OpenAPI / Scalar UI
app.MapHub<PaymentHub>("/hub");
app.MapOpenApi();
app.MapScalarApiReference();
app.MapGet("/", () => Results.Redirect("/scalar/v1"));

app.Run();
