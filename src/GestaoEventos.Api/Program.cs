using GestaoEventos.Api;
using GestaoEventos.Api.Middlewares;
using GestaoEventos.Application;
using GestaoEventos.Infrastructure;

using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

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
    app.UseExceptionHandler();

    // Configure the HTTP request pipeline.
    app.UsePresentation();

    app.UseSerilogRequestLogging();
    app.UseMiddleware<RequestContextLoggingMiddleware>();
    app.UseAuthorization();
    app.Run();
}