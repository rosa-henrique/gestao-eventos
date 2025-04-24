using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Tests.Eventos;

public class IngressoTests
{
    [Fact]
    public void CriarIngresso_ComSucesso()
    {
        // Arrange
        const string nome = "ingresso";
        var dataInicio = DateTime.UtcNow;

        // Act
        var resultadoCriarIngresso = Ingresso.Criar(nome, "teste", 10, 10);

        // Assert
        resultadoCriarIngresso.IsError.Should().BeFalse();
        resultadoCriarIngresso.Value.Tipo.Nome.Should().BeSameAs(nome);
    }

    [Fact]
    public void AtualizarIngresso_ComSucesso()
    {
        // Arrange
        const string nome = "ingresso";
        const string nomeAlterado = "ingresso alterado";
        var ingresso = Ingresso.Criar(nome, "teste", 10, 10).Value;

        // Act
        var resultadoAlterarIngresso = ingresso.Alterar(nomeAlterado, ingresso.Tipo.Descricao, ingresso.Preco, ingresso.Quantidade);

        // Assert
        resultadoAlterarIngresso.IsError.Should().BeFalse();
        ingresso.Tipo.Nome.Should().BeSameAs(nomeAlterado);
    }
}