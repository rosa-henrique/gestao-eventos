using ErrorOr;

namespace GestaoEventos.Domain.Eventos;

public class EventoDomainService
{
    public const int CapaciadeMinima = 1;

    public static ErrorOr<Success> ValidarEvento(Evento evento)
    {
        if (evento.Detalhes.DataHora < DateTime.UtcNow)
        {
            return Error.Failure(description: ErrosEvento.DataRetroativa);
        }

        if (evento.Detalhes.CapacidadeMaxima < CapaciadeMinima)
        {
            return Error.Failure(description: ErrosEvento.CapacidadeInvalida);
        }

        return Result.Success;
    }
}