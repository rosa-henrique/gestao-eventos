using EventFlow.Eventos.Api.Endpoints;
using EventFlow.Eventos.Application;
using EventFlow.Eventos.Infrastructure;

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

app.MapBuscar();
app.MapBuscarPorId();
app.MapCriar();
app.MapAlterar();
app.MapCancelar();

app.MapDefaultEndpoints();

app.Run();