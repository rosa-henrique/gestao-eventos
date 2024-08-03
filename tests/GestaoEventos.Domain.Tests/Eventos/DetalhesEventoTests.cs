using GestaoEventos.Domain.Eventos;
using GestaoEventos.TestCommon.Eventos;

namespace GestaoEventos.Domain.Tests.Eventos;

public class DetalhesEventoTests
{
    [Fact]
    public void Comparar_ComSucesso()
    {
        // Arrange
        var dataHora = DateTime.UtcNow.AddDays(7);
        var detalhe1 = EventoFactory.CriarEvento(dataHora: dataHora).Detalhes;
        var detalhe2 = EventoFactory.CriarEvento(dataHora: dataHora).Detalhes;

        // Act
        var resultado = detalhe1 == detalhe2;

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void Comparar_ComFalha()
    {
        // Arrange
        var dataHora = DateTime.UtcNow.AddDays(7);
        var detalhe1 = EventoFactory.CriarEvento(dataHora: dataHora).Detalhes;
        var detalhe2 = EventoFactory.CriarEvento(dataHora: dataHora.AddSeconds(1)).Detalhes;

        // Act
        var resultado = detalhe1 == detalhe2;

        // Assert
        Assert.False(resultado);
    }
}