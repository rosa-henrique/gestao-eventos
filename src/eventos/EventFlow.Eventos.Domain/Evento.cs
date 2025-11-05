using ErrorOr;

using EventFlow.Shared.Domain;

namespace EventFlow.Eventos.Domain;

public class Evento : Entity, IAggregateRoot
{
    public string Nome { get; private set; } = null!;
    public DateTime DataHoraInicio { get; private set; }
    public DateTime DataHoraFim { get; private set; }
    public string Localizacao { get; private set; } = null!;
    public int CapacidadeMaxima { get; private set; }
    public StatusEvento Status { get; private set; } = null!;
    public Guid CriadoPor { get; private set; }

    private Evento(string nome, DateTime dataHoraInicio, DateTime dataHoraFim, string localizacao, int capacidadeMaxima,
        StatusEvento status, Guid criadoPor, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Nome = nome;
        DataHoraInicio = dataHoraInicio;
        DataHoraFim = dataHoraFim;
        Localizacao = localizacao;
        CapacidadeMaxima = capacidadeMaxima;
        Status = status;
        CriadoPor = criadoPor;
    }

    public static ErrorOr<Evento> Criar(string nome, DateTime dataHoraInicio, DateTime dataHoraFim, string localizacao,
        int capacidadeMaxima, Guid criadoPor)
    {
        var resultadoValidacao = Validar(dataHoraInicio, dataHoraFim, capacidadeMaxima);
        if (resultadoValidacao.IsError)
        {
            return resultadoValidacao.Errors;
        }

        return new Evento(nome, dataHoraInicio, dataHoraFim, localizacao, capacidadeMaxima, StatusEvento.Pendente,
            criadoPor);
    }

    public ErrorOr<Success> Atualizar(string nome, DateTime dataHoraInicio, DateTime dataHoraFim, string localizacao,
        int capacidadeMaxima, StatusEvento status)
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

    private ErrorOr<Success> ValidarAlterarCancelar(StatusEvento? novoStatus = null)
    {
        if (DataHoraInicio < DateTime.UtcNow)
        {
            return ErrosEvento.NaoAlterarEventoPassado;
        }

        if (StatusEvento.StatusNaoPermitemAlteracao.Contains(Status))
        {
            return ErrosEvento.NaoPermiteAlteracao(Status);
        }

        if (novoStatus is not null && !StatusEvento.StatusPermitemAlterarDiretamente[Status].Contains(novoStatus))
        {
            return ErrosEvento.NaoPermiteAlteracaoDiretamente(Status, novoStatus);
        }

        return Result.Success;
    }

    private static ErrorOr<Success> Validar(DateTime dataHoraInicio, DateTime dataHoraFim, int capacidadeMaxima)
    {
        if (dataHoraInicio < DateTime.UtcNow)
        {
            return ErrosEvento.DataRetroativa;
        }

        if (dataHoraFim < dataHoraInicio)
        {
            return ErrosEvento.DataFinalMenorIgualFinal;
        }

        if (capacidadeMaxima < CapacidadeMinima)
        {
            return ErrosEvento.CapacidadeInvalida;
        }

        if (capacidadeMaxima < CapacidadeMinima)
        {
            return ErrosEvento.CapacidadeInvalida;
        }

        return Result.Success;
    }

    public const int CapacidadeMinima = 1;

    protected Evento() { }
}