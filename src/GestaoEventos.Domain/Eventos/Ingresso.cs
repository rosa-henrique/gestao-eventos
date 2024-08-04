using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public sealed class Ingresso : Entity
{
    internal Ingresso(string nome, string descricao, decimal preco, int quantidade)
    {
        Tipo = new TipoIngresso(nome, descricao);
        Preco = preco;
        Quantidade = quantidade;
    }

    public TipoIngresso Tipo { get; private set; } = null!;
    public decimal Preco { get; private set; }
    public int Quantidade { get; private set; }

    public static Ingresso Criar(string nome, string descricao, decimal preco, int quantidade)
    {
        return new Ingresso(nome, descricao, preco, quantidade);
    }

    private Ingresso() { }
}