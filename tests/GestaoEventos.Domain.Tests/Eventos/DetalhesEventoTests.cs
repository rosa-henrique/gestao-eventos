using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Tests.Eventos;

public class DetalhesEventoTests
{
    [Fact]
    public void Comparar_ComSucesso()
    {
        // Arrange
        var nome = "teste";
        var dataHora = DateTime.UtcNow.AddDays(7);
        var localizacao = "123";
        var capacidadeMaxima = 5;
        var detalhe1 = Evento.Criar(nome, dataHora, localizacao, capacidadeMaxima).Value.Detalhes;
        var detalhe2 = Evento.Criar(nome, dataHora, localizacao, capacidadeMaxima).Value.Detalhes;

        // Act
        var resultado = detalhe1 == detalhe2;

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void Comparar_ComFalha()
    {
        // Arrange
        var nome = "teste";
        var dataHora = DateTime.UtcNow.AddDays(7);
        var localizacao = "123";
        var capacidadeMaxima = 5;
        var detalhe1 = Evento.Criar(nome, dataHora, localizacao, capacidadeMaxima).Value.Detalhes;
        var detalhe2 = Evento.Criar(nome, dataHora.AddSeconds(1), localizacao, capacidadeMaxima).Value.Detalhes;

        // Act
        var resultado = detalhe1 == detalhe2;

        // Assert
        Assert.False(resultado);
    }
}