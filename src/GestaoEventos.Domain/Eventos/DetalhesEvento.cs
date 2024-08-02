using ErrorOr;

using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public class DetalhesEvento : ValueObject
{
    public string Nome { get; private set; } = null!;
    public DateTime DataHora { get; private set; }
    public string Localizacao { get; private set; } = null!;
    public int CapacidadeMaxima { get; private set; }

    internal DetalhesEvento(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima)
    {
        Nome = nome;
        DataHora = dataHora;
        Localizacao = localizacao;
        CapacidadeMaxima = capacidadeMaxima;
    }

    internal void Atualizar(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima)
    {
        Nome = nome;
        DataHora = dataHora;
        Localizacao = localizacao;
        CapacidadeMaxima = capacidadeMaxima;
    }

    private DetalhesEvento() { }

    public const int CapacidadeMinima = 1;

    // Implementação de ValueObject
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Nome;
        yield return DataHora;
        yield return Localizacao;
        yield return CapacidadeMaxima;
    }
}