using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AdicionarIngresso;

public record AdicionarIngressoCommand(string Nome, string Descricao, decimal Preco, int Quantidade, Guid EventoId)
    : IRequest<ErrorOr<IngressoResponse>>
{
}