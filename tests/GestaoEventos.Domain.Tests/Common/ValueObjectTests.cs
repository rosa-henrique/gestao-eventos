using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Tests.Common;

public class ValueObjectTests
{
    [Fact]
    public void ValidarIgual_DeveDarSucesso()
    {
        // Arrange
        var valor = "teste";
        var objeto1 = new Objeto(valor);
        var objeto2 = new Objeto(valor);

        // Act
        var resultado = objeto1 == objeto2;

        // Arrange
        Assert.True(resultado);
    }

    [Fact]
    public void ValidarIgual_NaoDeveDarSucesso()
    {
        // Arrange
        var valor = "teste";
        var objeto1 = new Objeto(valor);
        var objeto2 = new Objeto(valor + "1");

        // Act
        var resultado = objeto1 == objeto2;

        // Arrange
        Assert.False(resultado);
    }

    [Fact]
    public void ValidarDiferente_DeveDarSucesso()
    {
        // Arrange
        var valor = "teste";
        var objeto1 = new Objeto(valor);
        var objeto2 = new Objeto(valor + "1");

        // Act
        var resultado = objeto1 != objeto2;

        // Arrange
        Assert.True(resultado);
    }

    [Fact]
    public void ValidarIgual_DeveDarSucesso_ValoresNull()
    {
        // Arrange
        Objeto objeto1 = null!;
        Objeto objeto2 = null!;

        // Act
        var resultado = objeto1 == objeto2;

        // Arrange
        Assert.True(resultado);
    }

    [Fact]
    public void ValidarIgual_DeveDarFalha_UmDosValoresNull()
    {
        // Arrange
        var valor = "teste";
        var objeto = new Objeto(valor);

        // Act
#pragma warning disable SA1131 // Use readable conditions
        var resultado1 = null! == objeto;
#pragma warning restore SA1131 // Use readable conditions
        var resultado2 = objeto == (Objeto)null!;

        // Arrange
        Assert.False(resultado1);
        Assert.False(resultado2);
    }
}

public class Objeto(string valor) : ValueObject
{
    public string Valor { get; set; } = valor;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Valor;
    }
}