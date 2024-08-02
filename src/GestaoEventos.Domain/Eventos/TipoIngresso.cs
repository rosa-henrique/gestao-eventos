using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public class TipoIngresso : ValueObject
{
    internal TipoIngresso(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
    }

    public string Nome { get; private set; } = null!;
    public string Descricao { get; private set; } = null!;

    // Implementação de ValueObject
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Nome;
        yield return Descricao;
    }
}