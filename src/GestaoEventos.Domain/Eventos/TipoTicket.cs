using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public class TipoTicket : ValueObject
{
    internal TipoTicket(string nome, string descricao)
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