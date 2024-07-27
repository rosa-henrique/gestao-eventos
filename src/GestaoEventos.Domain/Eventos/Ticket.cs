using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public class Ticket : Entity
{
    internal Ticket(string nome, string descricao, decimal preco)
    {
        Tipo = new TipoTicket(nome, descricao);
        Preco = preco;
    }

    public TipoTicket Tipo { get; private set; } = null!;
    public decimal Preco { get; private set; }
}