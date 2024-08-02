using ErrorOr;

using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public sealed class Evento : Entity, IAggregateRoot
{
    public DetalhesEvento Detalhes { get; private set; } = null!;

    private Evento(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima, Guid? id = null)
            : base(id ?? Guid.NewGuid())
    {
        Detalhes = new DetalhesEvento(nome, dataHora, localizacao, capacidadeMaxima);
    }

    public static ErrorOr<Evento> Criar(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima)
    {
        var resultadoValidacao = Validar(dataHora, capacidadeMaxima);
        if (resultadoValidacao.IsError)
        {
            return resultadoValidacao.Errors;
        }

        return new Evento(nome, dataHora, localizacao, capacidadeMaxima);
    }

    public ErrorOr<Success> Atualizar(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima)
    {
        var validarAlterar = ValidarAlterarRemover;
        if (validarAlterar.IsError)
        {
            return validarAlterar.Errors;
        }

        var resultadoValidacao = Validar(dataHora, capacidadeMaxima);
        if (resultadoValidacao.IsError)
        {
            return resultadoValidacao.Errors;
        }

        Detalhes.Atualizar(nome, dataHora, localizacao, capacidadeMaxima);

        return Result.Success;
    }

    private static ErrorOr<Success> Validar(DateTime dataHora, int capacidadeMaxima)
    {
        if (dataHora < DateTime.UtcNow)
        {
            return Error.Failure(description: ErrosEvento.DataRetroativa);
        }

        if (capacidadeMaxima < DetalhesEvento.CapacidadeMinima)
        {
            return Error.Failure(description: ErrosEvento.CapacidadeInvalida);
        }

        return Result.Success;
    }

    public ErrorOr<Success> ValidarAlterarRemover
    {
        get
        {
            if (Detalhes.DataHora < DateTime.UtcNow)
            {
                return Error.Failure(description: ErrosEvento.NaoAlterarEventoPassado);
            }

            return Result.Success;
        }
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