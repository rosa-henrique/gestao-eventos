using ErrorOr;

using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public sealed class Sessao : Entity
{
    public string Nome { get; private set; } = null!;
    public DateTime DataHoraInicio { get; private set; }
    public DateTime DataHoraFim { get; private set; }

    private Sessao(string nome, DateTime dataHoraInicio, DateTime dataHoraFim, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Nome = nome;
        DataHoraInicio = dataHoraInicio;
        DataHoraFim = dataHoraFim;
    }

    public static ErrorOr<Sessao> Criar(string nome, DateTime dataHoraInicio, DateTime dataHoraFim)
    {
        return dataHoraInicio > dataHoraFim
            ? Error.Failure(description: ErrosEvento.SessaoDataFinalMenorIgualFinal)
            : new Sessao(nome, dataHoraInicio, dataHoraFim);
    }

    public ErrorOr<Success> Alterar(string nome, DateTime dataHoraInicio, DateTime dataHoraFim)
    {
        if (dataHoraInicio > dataHoraFim)
        {
            return Error.Failure(description: ErrosEvento.SessaoDataFinalMenorIgualFinal);
        }

        Nome = nome;
        DataHoraInicio = dataHoraInicio;
        DataHoraFim = dataHoraFim;

        return Result.Success;
    }

    internal void Alterar(Sessao sessao)
    {
        Nome = sessao.Nome;
        DataHoraInicio = sessao.DataHoraInicio;
        DataHoraFim = sessao.DataHoraFim;
    }

    private Sessao() { }

    // Métodos de domínio
}