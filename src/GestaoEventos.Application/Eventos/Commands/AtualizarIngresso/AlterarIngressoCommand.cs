using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AtualizarIngresso;

public record AlterarIngressoCommand(Guid Id, string Nome, string Descricao, decimal Preco, int Quantidade, Guid EventoId) : IRequest<ErrorOr<Ingresso>>
{
}