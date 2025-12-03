using EventFlow.Inventario.Api.Endpoints;
using EventFlow.Inventario.Api.Services;
using EventFlow.Inventario.Application;
using EventFlow.Inventario.Infrastructure;

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

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGrpcService<IngressoService>();

app.UseAuthorization();
app.UseAuthorization();

app.MapCriarIngressoEndpoint();

app.MapDefaultEndpoints();

app.Run();