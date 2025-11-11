using EventFlow.Inventario.Api.Endpoints;
using EventFlow.Inventario.Application;
using EventFlow.Inventario.Infrastructure;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddLogs().AddServiceDefaults();

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

app.MapGet("/", () => "Hello World!").RequireAuthorization();

app.MapCriarIngressoEndpoint();

app.MapDefaultEndpoints();

app.Run();