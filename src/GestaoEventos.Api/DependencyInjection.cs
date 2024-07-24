using Microsoft.OpenApi.Models;

namespace GestaoEventos.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "API Gestão Projetos",
                Description = "Uma API Web ASP.NET Core para gerenciar eventos e relacionados",
            });
        });
        services.AddProblemDetails();

        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.DocumentTitle = "API Gestão Projetos";
            });
        }

        app.UseHttpsRedirection();

        return app;
    }
}