using FluentValidation;

namespace EventFlow.Eventos.Application.Commands.Alterar;

public class AlterarRequestValidator : AbstractValidator<AlterarRequest>
{
    public AlterarRequestValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
    }
}