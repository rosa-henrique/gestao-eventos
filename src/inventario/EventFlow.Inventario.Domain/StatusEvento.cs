using Ardalis.SmartEnum;

namespace EventFlow.Inventario.Domain;

public class StatusEvento(string name, int value) : SmartEnum<StatusEvento>(name, value)
{
    public static readonly StatusEvento Pendente = new("Pendente", 0);
    public static readonly StatusEvento Confirmado = new("Confirmado", 1);
    public static readonly StatusEvento Cancelado = new("Cancelado", 2);
    public static readonly StatusEvento EmAndamento = new("EmAndamento", 3);
    public static readonly StatusEvento Concluido = new("Concluído", 4);

    public static readonly IReadOnlyCollection<StatusEvento> StatusPermiteCompra = [Confirmado, EmAndamento];
}