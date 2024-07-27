using ErrorOr;

using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public class Evento : Entity, IAggregateRoot
{
    public DetalhesEvento Detalhes { get; private set; } = null!;

    public static ErrorOr<Evento> Criar(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima)
    {
        var resultado = DetalhesEvento.Criar(nome, dataHora, localizacao, capacidadeMaxima);

        if (resultado.IsError)
        {
            return resultado.Errors;
        }

        return new Evento
        {
            Detalhes = resultado.Value,
        };
    }

    public ErrorOr<Success> Atualizar(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima)
    {
        var resultado = Detalhes.Atualizar(nome, dataHora, localizacao, capacidadeMaxima);

        return resultado;
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