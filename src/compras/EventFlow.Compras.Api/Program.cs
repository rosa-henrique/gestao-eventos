using EventFlow.Compras.Api.Endpoints;
using EventFlow.Compras.Application;
using EventFlow.Compras.Infrastructure;

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

app.MapDefaultEndpoints();

app.Run();
