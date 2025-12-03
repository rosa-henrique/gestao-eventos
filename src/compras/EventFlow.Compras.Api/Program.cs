using EventFlow.Compras.Api;
using EventFlow.Compras.Api.Endpoints;
using EventFlow.Compras.Application;
using EventFlow.Compras.Infrastructure;
using EventFlow.Inventario.Grpc;

using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddKeycloakAuthentication()
    .AddInfrastructure();

builder.Services.AddApplication();

var isHttps = builder.Configuration["DOTNET_LAUNCH_PROFILE"] == "https";

builder.Services.AddSingleton<IngressoClient>()
    .AddGrpcServiceReference<Ingresso.IngressoClient>($"{(isHttps ? "https" : "http")}://_Grpc.inventarioapi", failureStatus: HealthStatus.Degraded);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.UseAuthorization();

app.MapComprarIngressoEndpoint();

var a = app.Services.GetRequiredService<IngressoClient>();
await a.Test();

app.MapDefaultEndpoints();

app.Run();
