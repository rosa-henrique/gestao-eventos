using GestaoEventos.Application.Common.Interfaces;
using GestaoEventos.Domain.Compras;
using GestaoEventos.Domain.Eventos;
using GestaoEventos.Domain.Usuarios;
using GestaoEventos.Infrastructure.Persistence;
using GestaoEventos.Infrastructure.Persistence.Repositories;
using GestaoEventos.Infrastructure.Security;
using GestaoEventos.Infrastructure.Security.CurrentUserProvider;
using GestaoEventos.Infrastructure.Security.TokenGenerator;
using GestaoEventos.Infrastructure.Security.TokenValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoEventos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor()
            .AddPersistence(configuration)
            .AddAuthentication(configuration)
            .AddAuthorization();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("GestaoEventos")));

        services.AddScoped<ICompraIngressoDomainService, CompraIngressoDomainService>();

        services.AddScoped<IEventoRepository, EventoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ICompraIngressoRepository, CompraIngressoRepository>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddSingleton<ICurrentUserProvider, CurrentUserProvider>();
        services.AddSingleton<IAuthorizationService, AuthorizationService>();

        services
            .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        return services;
    }
}