using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.Adicionar;

public record AdicionarEventoCommand(string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima) : IRequest<ErrorOr<Evento>>
{
}