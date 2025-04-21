using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Application.Eventos.Common.Responses;

public record SessaoResponse(Guid Id, string Nome, DateTime DataHoraInicio, DateTime DataHoraFim)
{
    public static implicit operator SessaoResponse(Sessao sessao)
        => new(sessao.Id, sessao.Nome, sessao.DataHoraInicio, sessao.DataHoraFim);
}