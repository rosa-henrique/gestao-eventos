using EventFlow.Inventario.Domain;

namespace EventFlow.Inventario.Application.Response;

public record IngressoResponse(Guid Id, string Nome, string Descricao, decimal Preco, int QuantidadeTotal)
{
    public static implicit operator IngressoResponse(Ingresso ingresso)
        => new(ingresso.Id, ingresso.Nome, ingresso.Descricao, ingresso.Preco, ingresso.QuantidadeTotal);
}