using GestaoEventos.Api;
using GestaoEventos.Api.Middlewares;
using GestaoEventos.Application;
using GestaoEventos.Infrastructure;

using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog((context, loggerConfig) =>
        loggerConfig.ReadFrom.Configuration(context.Configuration));

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddPresentation();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    app.UsePresentation();

    app.UseSerilogRequestLogging();
    app.UseMiddleware<RequestContextLoggingMiddleware>();

    app.Run();
}