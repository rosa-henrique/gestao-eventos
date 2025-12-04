using ErrorOr;

using EventFlow.Inventario.Domain;
using EventFlow.Shared.UnitOfWork;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventFlow.Inventario.Application.Commands.ProcessarItens;

public class ProcessarItensRequestHandler(IServiceProvider serviceProvider) : IRequestHandler<ProcessarItensRequest, ErrorOr<IEnumerable<ProcessarItensRequestResponse>>>
{
    private const int TentativasMaximas = 5;

    public async Task<ErrorOr<IEnumerable<ProcessarItensRequestResponse>>> Handle(ProcessarItensRequest request, CancellationToken cancellationToken)
    {
        for (int tentativa = 0; tentativa < TentativasMaximas; tentativa++)
        {
            using var scope = serviceProvider.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var repository = scope.ServiceProvider.GetRequiredService<IIngressoRepository>();

            // 🔥 Cria strategy (pela infra, sem expor DbContext)
            var strategy = unitOfWork.CreateExecutionStrategy();

            try
            {
                // 🔥 TUDO envolvendo banco fica dentro do EXECUTE
                return await strategy.ExecuteAsync(async Task<ErrorOr<IEnumerable<ProcessarItensRequestResponse>>> () =>
                {
                    // Inicia transação
                    await unitOfWork.BeginTransactionAsync(cancellationToken);

                    var ingressos = await repository.BuscarPorIds(
                        request.IngressosCompra.Select(i => i.Key),
                        cancellationToken);

                    if (ingressos.Count != request.IngressosCompra.Count())
                    {
                        await unitOfWork.RollbackAsync(cancellationToken);
                        return ErrosIngresso.IngressosInformadosParaReservarInvalidos;
                    }

                    // Regra de domínio — validar reserva
                    foreach (var ingresso in ingressos)
                    {
                        var quantidade = request.IngressosCompra[ingresso.Id];

                        var retornoReserva = ingresso.Reservar(quantidade);

                        if (!retornoReserva.IsError)
                        {
                            continue;
                        }

                        await unitOfWork.RollbackAsync(cancellationToken);

                        return retornoReserva.Errors;
                    }

                    repository.Alterar(ingressos);

                    // Commit final
                    await unitOfWork.CommitAsync(cancellationToken);

                    // Sucesso — montar resposta
                    var resposta = ingressos.Select(i =>
                        new ProcessarItensRequestResponse(i.Id, i.Preco, i.EventoId));

                    return resposta.ToList();
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                // Tenta nova execução (novo DbContext no próximo loop)
                if (tentativa < TentativasMaximas - 1)
                {
                    continue;
                }

                // Limite atingido
                return ErrosIngresso.NaoFoiPossivelReservarIngressos;
            }
            catch (Exception)
            {
                // Falha inesperada = rollback e exceção sobe
                await unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }

        return ErrosIngresso.NaoFoiPossivelReservarIngressos;
    }
}