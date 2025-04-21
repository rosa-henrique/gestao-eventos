using ErrorOr;

using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public sealed class Evento : Entity, IAggregateRoot
{
    public string Nome { get; private set; } = null!;
    public DateTime DataHoraInicio { get; private set; }
    public DateTime DataHoraFim { get; private set; }
    public string Localizacao { get; private set; } = null!;
    public int CapacidadeMaxima { get; private set; }
    public StatusEvento Status { get; private set; } = null!;
    private readonly IList<Ingresso> _ingressos = [];
    public IReadOnlyCollection<Ingresso> Ingressos => [.. _ingressos];

    private readonly IList<Sessao> _sessoes = [];
    public IReadOnlyCollection<Sessao> Sessoes => [.. _sessoes];

    private Evento(string nome, DateTime dataHoraInicio, DateTime dataHoraFim, string localizacao, int capacidadeMaxima,
        StatusEvento status, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Nome = nome;
        DataHoraInicio = dataHoraInicio;
        DataHoraFim = dataHoraFim;
        Localizacao = localizacao;
        CapacidadeMaxima = capacidadeMaxima;
        Status = status;
    }

    public static ErrorOr<Evento> Criar(string nome, DateTime dataHoraInicio, DateTime dataHoraFim, string localizacao,
        int capacidadeMaxima)
    {
        var resultadoValidacao = Validar(dataHoraInicio, dataHoraFim, capacidadeMaxima);
        if (resultadoValidacao.IsError)
        {
            return resultadoValidacao.Errors;
        }

        return new Evento(nome, dataHoraInicio, dataHoraFim, localizacao, capacidadeMaxima, StatusEvento.Pendente);
    }

    public ErrorOr<Success> Atualizar(string nome, DateTime dataHoraInicio, DateTime dataHoraFim, string localizacao,
        int capacidadeMaxima, StatusEvento status)
    {
        if (_sessoes.Any() && (dataHoraInicio > _sessoes.Min(e => e.DataHoraInicio) ||
                               dataHoraFim < _sessoes.Min(e => e.DataHoraFim)))
        {
            return Error.Conflict(description: ErrosEvento.ConflitoSessoes);
        }

        var validarAlterar = ValidarAlterarCancelar(status);
        if (validarAlterar.IsError)
        {
            return validarAlterar.Errors;
        }

        var resultadoValidacao = Validar(dataHoraInicio, dataHoraFim, capacidadeMaxima);

        if (resultadoValidacao.IsError)
        {
            return resultadoValidacao.Errors;
        }

        Nome = nome;
        DataHoraInicio = dataHoraInicio;
        DataHoraFim = dataHoraFim;
        Localizacao = localizacao;
        CapacidadeMaxima = capacidadeMaxima;
        Status = status;

        return Result.Success;
    }

    public ErrorOr<Success> Cancelar()
    {
        var validarAlterar = ValidarAlterarCancelar();
        if (validarAlterar.IsError)
        {
            return validarAlterar.Errors;
        }

        Status = StatusEvento.Cancelado;
        return Result.Success;
    }

    public ErrorOr<Success> AtualizarStatus(StatusEvento status)
    {
        var validarAlterar = ValidarAlterarCancelar(status);
        if (validarAlterar.IsError)
        {
            return validarAlterar.Errors;
        }

        Status = status;
        return Result.Success;
    }

    public ErrorOr<Success> AdicionarIngresso(Ingresso ingresso)
    {
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Status));
        }

        if (_ingressos.Any(i => i.Tipo.Nome == ingresso.Tipo.Nome))
        {
            return Error.Conflict(description: ErrosEvento.NomeIngressoJaExiste);
        }

        if ((_ingressos.Sum(i => i.Quantidade) + ingresso.Quantidade) > CapacidadeMaxima)
        {
            return Error.Failure(description: ErrosEvento.QuantidadeTotalIngressosExcedeCapacidadeMaxima);
        }

        _ingressos.Add(ingresso);
        return Result.Success;
    }

    public ErrorOr<Success> AtualizarIngresso(Ingresso ingresso)
    {
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Status));
        }

        if (_ingressos.Any(i => i.Tipo.Nome == ingresso.Tipo.Nome && i.Id != ingresso.Id))
        {
            return Error.Conflict(description: ErrosEvento.NomeIngressoJaExiste);
        }

        if ((_ingressos.Where(i => i.Id != ingresso.Id).Sum(i => i.Quantidade) + ingresso.Quantidade) >
            CapacidadeMaxima)
        {
            return Error.Failure(description: ErrosEvento.QuantidadeTotalIngressosExcedeCapacidadeMaxima);
        }

        _ingressos.FirstOrDefault(i => i.Id == ingresso.Id)!.Alterar(ingresso);

        return Result.Success;
    }

    public ErrorOr<Success> RemoverIngresso(Ingresso ingresso)
    {
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Status));
        }

        _ingressos.Remove(ingresso);

        return Result.Success;
    }

    public ErrorOr<Success> AdicionarSessao(Sessao sessao)
    {
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Status));
        }

        if (DataHoraInicio > sessao.DataHoraInicio || DataHoraFim < sessao.DataHoraFim)
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
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Status));
        }

        if (DataHoraInicio > sessao.DataHoraInicio || DataHoraFim < sessao.DataHoraFim)
        {
            return Error.Failure(description: ErrosEvento.DataSessaoForaIntervaloEvento);
        }

        if (_sessoes.Where(i => i.Id != sessao.Id).Any(s =>
                sessao.DataHoraInicio < s.DataHoraFim && sessao.DataHoraFim > s.DataHoraInicio))
        {
            return Error.Failure(description: ErrosEvento.ConflitoDataHoraSessao);
        }

        _sessoes.FirstOrDefault(i => i.Id == sessao.Id)!.Alterar(sessao);

        return Result.Success;
    }

    public ErrorOr<Success> RemoverSessao(Sessao sessao)
    {
        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, Status));
        }

        _sessoes.Remove(sessao);

        return Result.Success;
    }

    internal ErrorOr<Success> ValidarAlterarCancelar(StatusEvento? novoStatus = null)
    {
        if (DataHoraInicio < DateTime.UtcNow)
        {
            return Error.Failure(description: ErrosEvento.NaoAlterarEventoPassado);
        }

        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Status))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAlteracao, Status));
        }

        if (novoStatus is not null && !StatusEvento.StatusPermitemAlterarDiretamente[Status].Contains(novoStatus))
        {
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAlteracaoDiretamente, Status.Name,
                novoStatus.Name));
        }

        return Result.Success;
    }

    private static ErrorOr<Success> Validar(DateTime dataHoraInicio, DateTime dataHoraFim, int capacidadeMaxima)
    {
        if (dataHoraInicio < DateTime.UtcNow)
        {
            return Error.Failure(description: ErrosEvento.DataRetroativa);
        }

        if (dataHoraFim < dataHoraInicio)
        {
            return Error.Failure(description: ErrosEvento.DataFinalMenorIgualFinal);
        }

        if (capacidadeMaxima < CapacidadeMinima)
        {
            return Error.Failure(description: ErrosEvento.CapacidadeInvalida);
        }

        if (capacidadeMaxima < CapacidadeMinima)
        {
            return Error.Failure(description: ErrosEvento.CapacidadeInvalida);
        }

        return Result.Success;
    }

    public const int CapacidadeMinima = 1;

    private Evento() { }
}