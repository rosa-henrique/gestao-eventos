namespace GestaoEventos.Application.Eventos.Commands.AlterarStatusEvento;

public record AlterarStatusCommand(Guid Id, int Status)
{
}