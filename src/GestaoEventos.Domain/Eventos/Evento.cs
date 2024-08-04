using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

using ErrorOr;

using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public sealed class Evento : Entity, IAggregateRoot
{
    public DetalhesEvento Detalhes { get; private set; } = null!;

    private readonly IList<Ingresso> _ingressos = [];
    public IReadOnlyCollection<Ingresso> Ingressos => [.. _ingressos];

    private Evento(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima, StatusEvento status, Guid? id = null)
            : base(id ?? Guid.NewGuid())
    {
        Detalhes = new DetalhesEvento(nome, dataHora, localizacao, capacidadeMaxima, status);
    }

    public static ErrorOr<Evento> Criar(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima)
    {
        var resultadoValidacao = Validar(dataHora, capacidadeMaxima);
        if (resultadoValidacao.IsError)
        {
            return resultadoValidacao.Errors;
        }

        return new Evento(nome, dataHora, localizacao, capacidadeMaxima, StatusEvento.Pendente);
    }

    public ErrorOr<Success> Atualizar(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima, StatusEvento status)
    {
        var validarAlterar = ValidarAlterarCancelar(status);
        if (validarAlterar.IsError)
        {
            return validarAlterar.Errors;
        }

        var resultadoValidacao = Validar(dataHora, capacidadeMaxima);
        if (resultadoValidacao.IsError)
        {
            return resultadoValidacao.Errors;
        }

        Detalhes.Atualizar(nome, dataHora, localizacao, capacidadeMaxima, status);

        return Result.Success;
    }

    public ErrorOr<Success> Cancelar()
    {
        var validarAlterar = ValidarAlterarCancelar();
        if (validarAlterar.IsError)
        {
            return validarAlterar.Errors;
        }

        Detalhes.AtualizarStatus(StatusEvento.Cancelado);
        return Result.Success;
    }

    public ErrorOr<Success> AtualizarStatus(StatusEvento status)
    {
        var validarAlterar = ValidarAlterarCancelar(status);
        if (validarAlterar.IsError)
        {
            return validarAlterar.Errors;
        }

        Detalhes.AtualizarStatus(status);
        return Result.Success;
    }

    public ErrorOr<Success> AdicionarIngresso(Ingresso ingresso)
    {
        if (_ingressos.Any(i => i.Tipo.Nome == ingresso.Tipo.Nome))
        {
            return Error.Conflict(description: ErrosEvento.NomeIngressoJaExiste);
        }

        _ingressos.Add(ingresso);
        return Result.Success;
    }

    internal ErrorOr<Success> ValidarAlterarCancelar(StatusEvento? novoStatus = null)
    {
        if (Detalhes.DataHora < DateTime.UtcNow)
        {
            return Error.Failure(description: ErrosEvento.NaoAlterarEventoPassado);
        }

        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Detalhes.Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAlteracao, Detalhes.Status));
        }

        if (novoStatus is not null && !StatusEvento.StatusPermitemAlterarDiretamente[Detalhes.Status].Contains(novoStatus))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAlteracaoDiretamente, Detalhes.Status.Name, novoStatus.Name));
        }

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

    private Evento() { }

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