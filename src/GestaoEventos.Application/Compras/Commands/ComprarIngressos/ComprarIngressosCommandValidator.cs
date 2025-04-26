using FluentValidation;

namespace GestaoEventos.Application.Compras.Commands.ComprarIngressos;

public class ComprarIngressosCommandValidator : AbstractValidator<ComprarIngressosCommand>
{
    public ComprarIngressosCommandValidator()
    {
    }
}

public class IngressosCompraValidator : AbstractValidator<IngressosCompra>
{
    public IngressosCompraValidator()
    {
    }
}