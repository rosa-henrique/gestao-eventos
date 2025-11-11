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
    }

    public static ErrorOr<Ingresso> Criar(string nome, string descricao, decimal preco, int quantidade, Evento evento)
    {
        if (!StatusEvento.StatusPermiteAlteracaoIngresso.Contains(evento.Status))
        {
            return ErrosIngresso.StatusEventoNaoPermiteAlteracaoIngresso(evento.Status);
        }

        return new Ingresso(nome, descricao, preco, quantidade, evento);
    }

    public ErrorOr<Success> Alterar(string nome, string descricao, decimal preco, int quantidade)
    {
        if (!StatusEvento.StatusPermiteAlteracaoIngresso.Contains(Evento.Status))
        {
            return ErrosIngresso.StatusEventoNaoPermiteAlteracaoIngresso(Evento.Status);
        }

        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        QuantidadeTotal = quantidade;

        return Result.Success;
    }

    private Ingresso() { }
}