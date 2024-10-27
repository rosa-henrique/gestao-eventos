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

    private readonly IList<Sessao> _sessoes = [];
    public IReadOnlyCollection<Sessao> Sessoes => [.. _sessoes];

    private Evento(DetalhesEvento detalhesEvento, Guid? id = null)
            : base(id ?? Guid.NewGuid())
    {
        Detalhes = detalhesEvento;
    }

    public static ErrorOr<Evento> Criar(string nome, DateTime dataHoraInicio, DateTime dataHorafim, string localizacao, int capacidadeMaxima)
    {
        var resultadoCriar = DetalhesEvento.Criar(nome, dataHoraInicio, dataHorafim, localizacao, capacidadeMaxima);
        if (resultadoCriar.IsError)
        {
            return resultadoCriar.Errors;
        }

        return new Evento(resultadoCriar.Value);
    }

    public ErrorOr<Success> Atualizar(string nome, DateTime dataHoraInicio, DateTime dataHorafim, string localizacao, int capacidadeMaxima, StatusEvento status)
    {
        if (_sessoes.Any() && (dataHoraInicio > _sessoes.Min(e => e.DataHoraInicio) || dataHorafim < _sessoes.Min(e => e.DataHoraFim)))
        {
            return Error.Conflict(description: ErrosEvento.ConflitoSessoes);
        }

        var resultadoAtualizar = Detalhes.Atualizar(nome, dataHoraInicio, dataHorafim, localizacao, capacidadeMaxima, status);

        if (resultadoAtualizar.IsError)
        {
            return resultadoAtualizar.Errors;
        }

        return Result.Success;
    }

    public ErrorOr<Success> Cancelar()
    {
        var validarAlterar = Detalhes.Cancelar();
        if (validarAlterar.IsError)
        {
            return validarAlterar.Errors;
        }

        return Result.Success;
    }

    public ErrorOr<Success> AtualizarStatus(StatusEvento status)
    {
        var validarAlterar = Detalhes.AtualizarStatus(status);
        if (validarAlterar.IsError)
        {
            return validarAlterar.Errors;
        }

        return Result.Success;
    }

    public ErrorOr<Success> AdicionarIngresso(Ingresso ingresso)
    {
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Detalhes.Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Detalhes.Status));
        }

        if (_ingressos.Any(i => i.Tipo.Nome == ingresso.Tipo.Nome))
        {
            return Error.Conflict(description: ErrosEvento.NomeIngressoJaExiste);
        }

        if ((_ingressos.Sum(i => i.Quantidade) + ingresso.Quantidade) > Detalhes.CapacidadeMaxima)
        {
            return Error.Failure(description: ErrosEvento.QuantidadeTotalIngressosExcedeCapacidadeMaxima);
        }

        _ingressos.Add(ingresso);
        return Result.Success;
    }

    public ErrorOr<Success> AtualizarIngresso(Ingresso ingresso)
    {
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Detalhes.Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Detalhes.Status));
        }

        if (_ingressos.Any(i => i.Tipo.Nome == ingresso.Tipo.Nome && i.Id != ingresso.Id))
        {
            return Error.Conflict(description: ErrosEvento.NomeIngressoJaExiste);
        }

        if ((_ingressos.Where(i => i.Id != ingresso.Id).Sum(i => i.Quantidade) + ingresso.Quantidade) > Detalhes.CapacidadeMaxima)
        {
            return Error.Failure(description: ErrosEvento.QuantidadeTotalIngressosExcedeCapacidadeMaxima);
        }

        _ingressos.FirstOrDefault(i => i.Id == ingresso.Id)!.Alterar(ingresso);

        return Result.Success;
    }

    public ErrorOr<Success> RemoverIngresso(Ingresso ingresso)
    {
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Detalhes.Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Detalhes.Status));
        }

        _ingressos.Remove(ingresso);

        return Result.Success;
    }

    public ErrorOr<Success> AdicionarSessao(Sessao sessao)
    {
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Detalhes.Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Detalhes.Status));
        }

        if (Detalhes.DataHoraInicio > sessao.DataHoraInicio || Detalhes.DataHoraFim < sessao.DataHoraFim)
        {
            return Error.Failure(description: ErrosEvento.DataSessaoForaIntervaloEvento);
        }

        if (_sessoes.Any(s => sessao.DataHoraInicio < s.DataHoraFim && sessao.DataHoraFim > s.DataHoraInicio))
        {
            return Error.Failure(description: ErrosEvento.ConflitoDataHoraSessao);
        }

        _sessoes.Add(sessao);
        return Result.Success;
    }

    public ErrorOr<Success> AtualizarSessao(Sessao sessao)
    {
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Detalhes.Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Detalhes.Status));
        }

        if (Detalhes.DataHoraInicio > sessao.DataHoraInicio || Detalhes.DataHoraFim < sessao.DataHoraFim)
        {
            return Error.Failure(description: ErrosEvento.DataSessaoForaIntervaloEvento);
        }

        if (_sessoes.Where(i => i.Id != sessao.Id).Any(s => sessao.DataHoraInicio < s.DataHoraFim && sessao.DataHoraFim > s.DataHoraInicio))
        {
            return Error.Failure(description: ErrosEvento.ConflitoDataHoraSessao);
        }

        _sessoes.FirstOrDefault(i => i.Id == sessao.Id)!.Alterar(sessao);

        return Result.Success;
    }

    public ErrorOr<Success> RemoverSessao(Sessao sessao)
    {
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Detalhes.Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Detalhes.Status));
        }

        _sessoes.Remove(sessao);

        return Result.Success;
    }

    private Evento() { }
}