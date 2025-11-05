using FluentValidation;

namespace EventFlow.Eventos.Application.Commands.Criar;

public class CriarRequestValidator : AbstractValidator<CriarRequest>
{
    public CriarRequestValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
    }
}