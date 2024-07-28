using FluentValidation;

namespace GestaoEventos.Application.Eventos.Commands.Adicionar;

public class AdicionarEventoValidator : AbstractValidator<AdicionarEventoCommand>
{
    public AdicionarEventoValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
    }
}