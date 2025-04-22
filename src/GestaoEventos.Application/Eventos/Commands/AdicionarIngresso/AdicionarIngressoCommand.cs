using ErrorOr;

using GestaoEventos.Application.Common.Security;
using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AdicionarIngresso;

[AuthorizeCriadorEvento("EventoId")]
public record AdicionarIngressoCommand(string Nome, string Descricao, decimal Preco, int Quantidade, Guid EventoId)
    : IRequest<ErrorOr<IngressoResponse>>
{
}