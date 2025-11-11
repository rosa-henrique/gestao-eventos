using ErrorOr;

using EventFlow.Inventario.Application.Response;

using MediatR;

namespace EventFlow.Inventario.Application.Commands.CriarIngresso;

public record CriarIngressoRequest(string Nome, string Descricao, decimal Preco, int QuantidadeTotal, Guid EventoId)
    : IRequest<ErrorOr<IngressoResponse>>;