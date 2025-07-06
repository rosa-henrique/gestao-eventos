using ErrorOr;

using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public sealed class Ingresso : Entity
{
    private Ingresso(string nome, string descricao, decimal preco, int quantidade, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Tipo = new TipoIngresso(nome, descricao);
        Preco = preco;
        Quantidade = quantidade;
    }

    private Ingresso() { }

    public TipoIngresso Tipo { get; private set; } = null!;
    public decimal Preco { get; private set; }
    public int Quantidade { get; private set; }

    public static ErrorOr<Ingresso> Criar(string nome, string descricao, decimal preco, int quantidade)
    {
        return new Ingresso(nome, descricao, preco, quantidade);
    }

    public ErrorOr<Success> Alterar(string nome, string descricao, decimal preco, int quantidade)
    {
        Tipo = new TipoIngresso(nome, descricao);
        Preco = preco;
        Quantidade = quantidade;

        return Result.Success;
    }

    internal void Alterar(Ingresso novosDados)
    {
        Tipo = new TipoIngresso(novosDados.Tipo.Nome, novosDados.Tipo.Nome);
        Preco = novosDados.Preco;
        Quantidade = novosDados.Quantidade;
    }
}