namespace GestaoEventos.Domain.Eventos;

public class EventoDomainService(IEventoRepository eventoRepository)
{
    public async Task CriarEvento(Evento evento)
    {
        await eventoRepository.Adicionar(evento);
    }
}