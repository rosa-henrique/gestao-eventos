using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Tests.Eventos;

public class DetalhesEventoTests
{
    [Fact]
    public void Comparar_ComSucesso()
    {
        // Arrange
        var dataHora = DateTime.UtcNow.AddDays(7);
        var detalhe1 = MockEvento.Novo(dataHora: dataHora).Detalhes;
        var detalhe2 = MockEvento.Novo(dataHora: dataHora).Detalhes;

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
        var capacidadeMaxima = 5;
        var detalhe1 = MockEvento.Novo(dataHora: dataHora, capacidadeMaxima: capacidadeMaxima).Detalhes;
        var detalhe2 = MockEvento.Novo(dataHora: dataHora, capacidadeMaxima: capacidadeMaxima + 5).Detalhes;

        // Act
        var resultado = detalhe1 == detalhe2;

        // Assert
        Assert.False(resultado);
    }
}