using ErrorOr;

using GestaoEventos.Application.Common.Interfaces;
using GestaoEventos.Domain.Compras;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Compras.Commands.ComprarIngressos;

public class ComprarIngressosCommandHandler(
    IEventoRepository eventoRepository,
    ICompraIngressoRepository compraIngressoRepository,
    ICompraIngressoDomainService compraIngressoDomainService,
    IAuthorizationService authorizationService)
    : IRequestHandler<ComprarIngressosCommand, ErrorOr<Success>>
{
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public async Task<ErrorOr<Success>> Handle(ComprarIngressosCommand request, CancellationToken cancellationToken)
    {
        var eventoPorSessao = await eventoRepository.BuscarPorSessao(request.SessaoId, cancellationToken);
        if (eventoPorSessao is null)
        {
            return Error.NotFound(description: ErrosCompras.SessaoNaoEncontrada);
        }

        var sessao = eventoPorSessao.Sessoes.FirstOrDefault(s => s.Id == request.SessaoId);
        var usuarioId = authorizationService.ObterIdUsuario();

        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            var compra = await compraIngressoDomainService.RealizarCompra(eventoPorSessao.Ingressos, request.SessaoId,
                usuarioId, request.IngressosCompra.ToDictionary(i => i.IngressoId, i => i.Quantidade),
                cancellationToken);

            if (compra.IsError)
            {
                return compra.Errors;
            }

            compraIngressoRepository.Adicionar(compra.Value);
            await compraIngressoRepository.SaveChangesAsync(cancellationToken);
        }
        finally
        {
            _semaphore.Release();
        }

        return Result.Success;
    }
}