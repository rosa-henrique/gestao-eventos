﻿using ErrorOr;

using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public class DetalhesEvento : ValueObject
{
    public string Nome { get; private set; } = null!;
    public DateTime DataHoraInicio { get; private set; }
    public DateTime DataHoraFim { get; private set; }
    public string Localizacao { get; private set; } = null!;
    public int CapacidadeMaxima { get; private set; }
    public StatusEvento Status { get; private set; } = null!;

    private DetalhesEvento(string nome, DateTime dataHoraInicio, DateTime dataHoraFim, string localizacao, int capacidadeMaxima, StatusEvento statusEvento)
    {
        Nome = nome;
        DataHoraInicio = dataHoraInicio;
        DataHoraFim = dataHoraFim;
        Localizacao = localizacao;
        CapacidadeMaxima = capacidadeMaxima;
        Status = statusEvento;
    }

    public static ErrorOr<DetalhesEvento> Criar(string nome, DateTime dataHoraInicio, DateTime dataHoraFim, string localizacao, int capacidadeMaxima)
    {
        var resultadoValidacao = Validar(dataHoraInicio, dataHoraFim, capacidadeMaxima);
        if (resultadoValidacao.IsError)
        {
            return resultadoValidacao.Errors;
        }

        return new DetalhesEvento(nome, dataHoraInicio, dataHoraFim, localizacao, capacidadeMaxima, StatusEvento.Pendente);
    }

    public ErrorOr<Success> Atualizar(string nome, DateTime dataHoraInicio, DateTime dataHoraFim, string localizacao, int capacidadeMaxima, StatusEvento status)
    {
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
            return Error.Failure(description: string.Format(ErrosEvento.NaoPermiteAlteracaoDiretamente, Status.Name, novoStatus.Name));
        }

        return Result.Success;
    }

    internal ErrorOr<Success> Cancelar()
    {
        var validarAlterar = ValidarAlterarCancelar();
        if (validarAlterar.IsError)
        {
            return validarAlterar.Errors;
        }

        Status = StatusEvento.Cancelado;
        return Result.Success;
    }

    internal ErrorOr<Success> AtualizarStatus(StatusEvento status)
    {
        var validarAlterar = ValidarAlterarCancelar(status);
        if (validarAlterar.IsError)
        {
            return validarAlterar.Errors;
        }

        Status = status;
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

    private DetalhesEvento() { }

    public const int CapacidadeMinima = 1;

    // Implementação de ValueObject
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Nome;
        yield return DataHoraInicio;
        yield return DataHoraFim;
        yield return Localizacao;
        yield return CapacidadeMaxima;
        yield return Status;
    }
}