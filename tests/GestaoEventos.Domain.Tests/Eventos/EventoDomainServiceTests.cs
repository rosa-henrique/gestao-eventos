using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Tests.Eventos;

public class EventoDomainServiceTests
{
    [Fact]
    public void ValidarEvento_ComSucesso()
    {
        // Arrange
        var evento = MockEvento.Novo();

        // Act
        var resultadoValidacao = EventoDomainService.ValidarEvento(evento);

        // Assert
        Assert.False(resultadoValidacao.IsError);
    }

    [Fact]
    public void ValidarEvento_ComErro_DataRetroativa()
    {
        // Arrange
        var evento = MockEvento.Novo(dataHora: DateTime.Now.AddDays(-1));

        // Act
        var resultadoValidacao = EventoDomainService.ValidarEvento(evento);

        // Assert
        Assert.True(resultadoValidacao.IsError);
        Assert.Equal(ErrosEvento.DataRetroativa, resultadoValidacao.Errors.FirstOrDefault().Description);
    }

    [Fact]
    public void ValidarEvento_ComErro_CapacidadeInvalida()
    {
        // Arrange
        var evento = MockEvento.Novo(capacidadeMaxima: 0);

        // Act
        var resultadoValidacao = EventoDomainService.ValidarEvento(evento);

        // Assert
        Assert.True(resultadoValidacao.IsError);
        Assert.Equal(ErrosEvento.CapacidadeInvalida, resultadoValidacao.Errors.FirstOrDefault().Description);
    }
}