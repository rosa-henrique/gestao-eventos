using Ardalis.SmartEnum;

namespace GestaoEventos.Domain.Eventos;

public class StatusEvento(string name, int value) : SmartEnum<StatusEvento>(name, value)
{
    public static readonly StatusEvento Pendente = new("Pendente", 0);
    public static readonly StatusEvento Confirmado = new("Confirmado", 1);
    public static readonly StatusEvento Cancelado = new("Cancelado", 2);
    public static readonly StatusEvento EmAndamento = new("Em Andamento", 3);
    public static readonly StatusEvento Concluido = new("Concluído", 4);

    public static readonly IReadOnlyCollection<StatusEvento> StatusNaoPermitemAlteracao = [Cancelado, Concluido];
    public static readonly IReadOnlyCollection<StatusEvento> StatusPermiteCompra = [Confirmado, EmAndamento];

    public static readonly Dictionary<StatusEvento, IList<StatusEvento>> StatusPermitemAlterarDiretamente = new()
    {
        { Pendente, [Pendente, Confirmado, Cancelado] },
        { Confirmado, [Confirmado, Cancelado, Cancelado, EmAndamento] },
        { EmAndamento, [EmAndamento, Cancelado, Concluido] },
    };
}