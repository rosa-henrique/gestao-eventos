using System.Reflection;

using GestaoEventos.Contracts.Eventos;
using GestaoEventos.Domain.Eventos;

using Mapster;

namespace GestaoEventos.Api.Extensions;

public static class MappingExtension
{
    public static IServiceCollection AddMapping(this IServiceCollection services, Assembly assembly)
    {
        TypeAdapterConfig<Evento, EventoDto>
            .NewConfig()
            .Map(member => member.CapacidadeMaxima, src => src.Detalhes.CapacidadeMaxima)
            .Map(member => member.Nome, src => src.Detalhes.Nome)
            .Map(member => member.Localizacao, src => src.Detalhes.Localizacao)
            .Map(member => member.DataHora, src => src.Detalhes.DataHora);

        TypeAdapterConfig.GlobalSettings.Scan(assembly);
        return services;
    }
}