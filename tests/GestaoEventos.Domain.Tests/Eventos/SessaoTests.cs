using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Tests.Eventos;

public class SessaoTests
{
    [Fact]
    public void CriarSessao_ComSucesso()
    {
        // Arrange
        const string nome = "sessao";
        var dataInicio = DateTime.UtcNow;

        // Act
        var resultadoCriarSessao = Sessao.Criar(nome, dataInicio, dataInicio.AddMinutes(5));

        // Assert
        resultadoCriarSessao.IsError.Should().BeFalse();
        resultadoCriarSessao.Value.Nome.Should().BeSameAs(nome);
    }

    [Fact]
    public void CriarSessao_ComErro_DataFinalMenorIgualFinal()
    {
        // Arrange
        const string nome = "sessao";
        var dataInicio = DateTime.UtcNow;

        // Act
        var resultadoCriarSessao = Sessao.Criar(nome, dataInicio, dataInicio.AddMinutes(-5));

        // Assert
        resultadoCriarSessao.IsError.Should().BeTrue();
        resultadoCriarSessao.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.SessaoDataFinalMenorIgualFinal);
    }

    [Fact]
    public void AlterarSessao_ComSucesso()
    {
        // Arrange
        const string nome = "sessao";
        const string nomeAlterado = "sessao alterada";
        var dataInicio = DateTime.UtcNow;
        var sessao = Sessao.Criar(nome, dataInicio, dataInicio.AddMinutes(5)).Value;

        // Act
        var resultadoAlterarSessao = sessao.Alterar(nomeAlterado, sessao.DataHoraInicio, sessao.DataHoraFim);

        // Assert
        resultadoAlterarSessao.IsError.Should().BeFalse();
        sessao.Nome.Should().BeSameAs(nomeAlterado);
    }

    [Fact]
    public void AlterarSessao_ComErro_DataFinalMenorIgualFinal()
    {
        // Arrange
        const string nome = "sessao";
        const string nomeAlterado = "sessao alterada";
        var dataInicio = DateTime.UtcNow;
        var sessao = Sessao.Criar(nome, dataInicio, dataInicio.AddMinutes(5)).Value;

        // Act
        var resultadoAlterarSessao =
            sessao.Alterar(nomeAlterado, sessao.DataHoraInicio, sessao.DataHoraInicio.AddMinutes(-5));

        // Assert
        resultadoAlterarSessao.IsError.Should().BeTrue();
        resultadoAlterarSessao.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.SessaoDataFinalMenorIgualFinal);
    }
}