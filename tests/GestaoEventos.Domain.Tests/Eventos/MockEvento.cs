using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Tests.Eventos;

public class MockEvento
{
    public const string Nome = "teste";
    public const string Localizacao = "Rua teste";

    public static Evento Novo(
        string nome = Nome,
        DateTime? dataHora = null,
        string localizacao = Localizacao,
        int? capacidadeMaxima = null)
    {
        return new Evento(nome, dataHora ?? DateTime.Now.AddDays(1), localizacao, capacidadeMaxima ?? EventoDomainService.CapaciadeMinima);
    }
}