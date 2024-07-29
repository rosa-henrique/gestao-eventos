using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public sealed class Evento : Entity, IAggregateRoot
{
    public DetalhesEvento Detalhes { get; private set; } = null!;

    public Evento(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima)
    {
        Detalhes = new DetalhesEvento(nome, dataHora, localizacao, capacidadeMaxima);
    }

    private Evento() { }

    // private readonly HashSet<Ticket> _tickets = [];
    // private readonly HashSet<Sessao> _sessoes = [];

    // public ErrorOr<Success> AdicionarSessao(string nome, DateTime inicio, DateTime fim)
    // {
    //    var resultado = Sessao.Criar(nome, inicio, fim);

    // if (!resultado.IsError)
    //    {
    //        return resultado.Errors;
    //    }

    // _sessoes.Add(resultado.Value);

    // return Result.Success;
    // }

    // public void AdicionarTicket(string nome, string descricao, decimal preco)
    // {
    //    var ticket = new Ticket(nome, descricao, preco);
    //    _tickets.Add(ticket);
    // }

    // Métodos de domínio
}