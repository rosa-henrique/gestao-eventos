using ErrorOr;

using EventFlow.Shared.Domain;

namespace EventFlow.Inventario.Domain;

public class Ingresso : Entity, IAggregateRoot
{
    public Guid EventoId { get; private set; }
    public string Nome { get; private set; } = null!;
    public string Descricao { get; private set; } = null!;
    public decimal Preco { get; private set; }
    public int QuantidadeTotal { get; private set; }

    public virtual Evento Evento { get; private set; } = null!;

    private Ingresso(string nome, string descricao, decimal preco, int quantidade, Evento evento)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        QuantidadeTotal = quantidade;
        EventoId = evento.Id;
        Evento = evento;
    }

    public static ErrorOr<Ingresso> Criar(string nome, string descricao, decimal preco, int quantidade, Evento evento)
    {
        if (!StatusEvento.StatusPermiteCompra.Contains(evento.Status))
        {
            return ErrosIngresso.EventoNaoPermiteCompraIngresso;
        }

        return new Ingresso(nome, descricao, preco, quantidade, evento);
    }

    public void Alterar(string nome, string descricao, decimal preco, int quantidade)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        QuantidadeTotal = quantidade;
    }

    private Ingresso() { }
}