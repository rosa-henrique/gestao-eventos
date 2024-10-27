using GestaoEventos.TestCommon.Eventos;

namespace GestaoEventos.Domain.Tests.Eventos;

public class TipoIngressoTests
{
    [Fact]
    public void Comparar_ComSucesso()
    {
        // Arrange
        const string nome = "";
        const string descricao = "";
        var tipoIngresso1 = TipoIngressoFactory.CriarTipoIngresso(nome, descricao);
        var tipoIngresso2 = TipoIngressoFactory.CriarTipoIngresso(nome, descricao);

        // Act
        var resultado = tipoIngresso1 == tipoIngresso2;

        // Assert
        resultado.Should().BeTrue();
    }

    [Fact]
    public void Comparar_ComErro()
    {
        // Arrange
        const string nome = "";
        const string descricao = "";
        var tipoIngresso1 = TipoIngressoFactory.CriarTipoIngresso(nome, descricao);
        var tipoIngresso2 = TipoIngressoFactory.CriarTipoIngresso(nome, descricao + " ");

        // Act
        var resultado = tipoIngresso1 == tipoIngresso2;

        // Assert
        resultado.Should().BeFalse();
    }
}