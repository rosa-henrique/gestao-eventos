using ErrorOr;

using EventFlow.Compras.Application.Dtos;

namespace EventFlow.Compras.Application.Interfaces;

public interface IIngressoClient
{
    Task<ErrorOr<IEnumerable<ProcessarItensResponseDto>>> ProcessarItens(IDictionary<string, int> dadosRequest);
}