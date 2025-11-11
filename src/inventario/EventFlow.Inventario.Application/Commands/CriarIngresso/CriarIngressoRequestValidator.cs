using FluentValidation;

namespace EventFlow.Inventario.Application.Commands.CriarIngresso;

public class CriarIngressoRequestValidator : AbstractValidator<CriarIngressoRequest>
{
    public CriarIngressoRequestValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Descricao).NotEmpty();
    }
}