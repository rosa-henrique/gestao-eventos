using GestaoEventos.Api;
using GestaoEventos.Application;
using GestaoEventos.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services
        .AddApplication()
        .AddInfrastructure()
        .AddPresentation();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    app.UsePresentation();

    app.Run();
}