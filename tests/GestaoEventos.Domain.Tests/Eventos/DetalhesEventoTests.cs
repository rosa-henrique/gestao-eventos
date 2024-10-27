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
        var detalhe1 = DetalheEventoFactory.CriarDetalheEvento(dataHoraInicio: dataHora, dataHoraFim: dataHora);
        var detalhe2 = DetalheEventoFactory.CriarDetalheEvento(dataHoraInicio: dataHora, dataHoraFim: dataHora);

        // Act
        var resultado = detalhe1 == detalhe2;

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void Comparar_ComSucesso_Equals()
    {
        // Arrange
        var dataHora = DateTime.UtcNow.AddDays(7);
        var detalhe1 = DetalheEventoFactory.CriarDetalheEvento(dataHoraInicio: dataHora, dataHoraFim: dataHora);
        var detalhe2 = DetalheEventoFactory.CriarDetalheEvento(dataHoraInicio: dataHora, dataHoraFim: dataHora);

        var a = detalhe2.GetHashCode();

        // Act
        var resultado = detalhe1.Equals((object)detalhe2);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void GetHashCodeTest()
    {
        // Arrange
        var dataHora = DateTime.UtcNow.AddDays(7);

        // Act
        var hashCode = DetalheEventoFactory.CriarDetalheEvento(dataHoraInicio: dataHora, dataHoraFim: dataHora)
            .GetHashCode();

        // Assert
        hashCode.Should().NotBe(0);
    }

    [Fact]
    public void Comparar_ComFalha()
    {
        // Arrange
        var dataHora = DateTime.UtcNow.AddDays(7);
        var detalhe1 = DetalheEventoFactory.CriarDetalheEvento(dataHoraInicio: dataHora);
        var detalhe2 = DetalheEventoFactory.CriarDetalheEvento(dataHoraInicio: dataHora.AddSeconds(1));

        // Act
        var resultado = detalhe1 == detalhe2;

        // Assert
        Assert.False(resultado);
    }
}