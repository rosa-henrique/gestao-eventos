namespace GestaoEventos.Api.Endpoints.Eventos.Request;

public record AdicionarSessaoRequest(string Nome, DateTime DataHoraInicio, DateTime DataHoraFim)
{
}