using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Application.Eventos.Common.Responses;

public record IngressoResponse(Guid Id, string Nome, string Descricao, decimal Preco, int Quantidade)
{
    public static implicit operator IngressoResponse(Ingresso ingresso)
        => new(ingresso.Id, ingresso.Tipo.Nome, ingresso.Tipo.Descricao, ingresso.Preco, ingresso.Quantidade);
}