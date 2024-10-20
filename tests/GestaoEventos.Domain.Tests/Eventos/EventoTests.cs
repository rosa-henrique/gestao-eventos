using FluentAssertions.Equivalency;
using FluentAssertions.Execution;

using GestaoEventos.Domain.Eventos;
using GestaoEventos.TestCommon.Eventos;

namespace GestaoEventos.Domain.Tests.Eventos;

public class EventoTests
{
    [Fact]
    public void CriarEvento_ComSucesso()
    {
        // Arrange
        var nome = "teste";
        var dataHora = DateTime.UtcNow.AddDays(7);
        var localizacao = "123";
        var capacidadeMaxima = 5;

        // Act
        var resultadoEvento = Evento.Criar(nome, dataHora, localizacao, capacidadeMaxima);

        // Assert
        resultadoEvento.IsError.Should().BeFalse();
        resultadoEvento.Value.Detalhes.Should().NotBeNull()
                                    .And.BeEquivalentTo(new
                                    {
                                        Nome = nome,
                                        DataHora = dataHora,
                                        Localizacao = localizacao,
                                        CapacidadeMaxima = capacidadeMaxima,
                                        Status = StatusEvento.Pendente,
                                    });
    }

    [Fact]
    public void CriarEvento_ComErro_CapacidadeInvalida()
    {
        // Arrange
        var nome = "teste";
        var dataHora = DateTime.UtcNow.AddDays(7);
        var localizacao = "123";
        var capacidadeMaxima = DetalhesEvento.CapacidadeMinima - 5;

        // Act
        var resultadoEvento = Evento.Criar(nome, dataHora, localizacao, capacidadeMaxima);

        // Assert
        resultadoEvento.IsError.Should().BeTrue();
        resultadoEvento.Errors.Should().NotBeEmpty()
                                                .And.Satisfy(a => a.Description == ErrosEvento.CapacidadeInvalida);
    }

    [Fact]
    public void CriarEvento_ComErro_DataRetroativa()
    {
        // Arrange
        var nome = "teste";
        var dataHora = DateTime.UtcNow.AddDays(-7);
        var localizacao = "123";
        var capacidadeMaxima = DetalhesEvento.CapacidadeMinima + 5;

        // Act
        var resultadoEvento = Evento.Criar(nome, dataHora, localizacao, capacidadeMaxima);

        // Assert
        resultadoEvento.IsError.Should().BeTrue();
        resultadoEvento.Errors.Should().NotBeEmpty()
                                                .And.Satisfy(a => a.Description == ErrosEvento.DataRetroativa);
    }

    [Fact]
    public void AtualizarEvento_ComSucesso()
    {
        // Arrange
        var nome = "teste";
        var dataHora = DateTime.UtcNow.AddDays(7);
        var localizacao = "123";
        var capacidadeMaxima = DetalhesEvento.CapacidadeMinima + 5;
        var evento = EventoFactory.CriarEvento();

        // Act
        var resultadoAtualizarEvento = evento.Atualizar(nome, dataHora, localizacao, capacidadeMaxima, StatusEvento.Pendente);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeFalse();
        evento.Detalhes.Should().NotBeNull()
                                     .And.BeEquivalentTo(new
                                     {
                                         Nome = nome,
                                         DataHora = dataHora,
                                         Localizacao = localizacao,
                                         CapacidadeMaxima = capacidadeMaxima,
                                     });
    }

    [Fact]
    public void AtualizarEvento_ComErro_CapacidadeInvalida()
    {
        // Arrange
        var nome = "teste";
        var dataHora = DateTime.UtcNow.AddDays(7);
        var localizacao = "123";
        var capacidadeMaxima = DetalhesEvento.CapacidadeMinima - 5;
        var evento = EventoFactory.CriarEvento();

        // Act
        var resultadoAtualizarEvento = evento.Atualizar(nome, dataHora, localizacao, capacidadeMaxima, StatusEvento.Pendente);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
                                                .And.Satisfy(a => a.Description == ErrosEvento.CapacidadeInvalida);
    }

    [Fact]
    public void AtualizarEvento_ComErro_DataRetroativa()
    {
        // Arrange
        var nome = "teste";
        var dataHora = DateTime.UtcNow.AddDays(-7);
        var localizacao = "123";
        var capacidadeMaxima = DetalhesEvento.CapacidadeMinima + 5;
        var evento = EventoFactory.CriarEvento();

        // Act
        var resultadoAtualizarEvento = evento.Atualizar(nome, dataHora, localizacao, capacidadeMaxima, StatusEvento.Pendente);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
                                                .And.Satisfy(a => a.Description == ErrosEvento.DataRetroativa);
    }

    [Fact]
    public void AtualizarEvento_ComErro_EventoJaPassou()
    {
        // Arrange
        var eventoPassado = EventoFactory.CriarEvento(dataHora: DateTime.Now.AddDays(-8));

        // Act
        var resultadoAtualizarEvento = eventoPassado.Atualizar("nome", DateTime.Now, "localizacao", 5, StatusEvento.Pendente);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
                                                .And.Satisfy(a => a.Description == ErrosEvento.NaoAlterarEventoPassado);
    }

    [Fact]
    public void CancelarEvento_ComSucesso()
    {
        // Arrange
        var evento = EventoFactory.CriarEvento();

        // Act
        var resultadoAtualizarEvento = evento.Cancelar();

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeFalse();
        evento.Detalhes.Status.Should().BeSameAs(StatusEvento.Cancelado);
    }

    [Fact]
    public void CancelarEvento_ComErro_EventoJaPassou()
    {
        // Arrange
        var eventoPassado = EventoFactory.CriarEvento(dataHora: DateTime.Now.AddDays(-8));

        var resultadoAtualizarEvento = eventoPassado.Cancelar();

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
                                                .And.Satisfy(a => a.Description == ErrosEvento.NaoAlterarEventoPassado);
    }

    [Fact]
    public void AtualizarStatusEvento_ComSucesso()
    {
        // Arrange
        var evento = EventoFactory.CriarEvento();
        var novoStatus = StatusEvento.Confirmado;

        // Act
        var resultadoAtualizarEvento = evento.AtualizarStatus(novoStatus);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeFalse();
        evento.Detalhes.Status.Should().BeSameAs(novoStatus);
    }

    [Fact]
    public void AtualizarStatusEvento_ComErro_EventoCanceladoConcluido()
    {
        // Arrange
        var statusImpedido = StatusEvento.Cancelado;
        var evento = EventoFactory.CriarEvento(status: statusImpedido);
        var msgErro = string.Format(ErrosEvento.NaoPermiteAlteracao, statusImpedido);

        // Act
        var resultadoAtualizarEvento = evento.AtualizarStatus(StatusEvento.Confirmado);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
                                                .And.Satisfy(a => a.Description == msgErro);
    }

    [Fact]
    public void AtualizarStatusEvento_ComErro_StatusNaoPodemAlterarDiretamente()
    {
        // Arrange
        var statusImpedido = StatusEvento.Pendente;
        var novoStatusImpedido = StatusEvento.EmAndamento;
        var evento = EventoFactory.CriarEvento(status: statusImpedido);
        var msgErro = string.Format(ErrosEvento.NaoPermiteAlteracaoDiretamente, statusImpedido, novoStatusImpedido);

        // Act
        var resultadoAtualizarEvento = evento.AtualizarStatus(novoStatusImpedido);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
                                                .And.Satisfy(a => a.Description == msgErro);
    }

    [Fact]
    public void AdicionarIngresso_ComSucesso()
    {
        // Arrange
        var nomeIngresso = "ingresso";
        var evento = EventoFactory.CriarEvento();
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10);

        // Act
        var resultadoAtualizarEvento = evento.AdicionarIngresso(ingresso);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeFalse();
        evento.Ingressos.Should().NotBeEmpty()
                                .And.Satisfy(a => a.Tipo.Nome == nomeIngresso);
    }

    [Fact]
    public void AdicionarIngresso_ComErro_NomeIngressoJaExiste()
    {
        // Arrange
        var nomeIngresso = "ingresso";
        var evento = EventoFactory.CriarEvento();
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10);
        evento.AdicionarIngresso(ingresso);

        // Act
        var resultadoAtualizarEvento = evento.AdicionarIngresso(Ingresso.Criar(nomeIngresso, "descricao ingresso 2", 2, 2));

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
                                .And.Satisfy(a => a.Description == ErrosEvento.NomeIngressoJaExiste);
    }

    [Fact]
    public void AdicionarIngresso_ComErro_NaoPermiteAdicaoIngresso()
    {
        // Arrange
        var msgErro = string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, StatusEvento.Cancelado);
        var evento = EventoFactory.CriarEvento(status: StatusEvento.Cancelado);
        var ingresso = Ingresso.Criar("ingresso", "descricao ingresso", 10, 10);

        // Act
        var resultadoAtualizarEvento = evento.AdicionarIngresso(ingresso);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
                                .And.Satisfy(a => a.Description == msgErro);
    }

    [Fact]
    public void AdicionarIngresso_ComErro_QuantidadeTotalIngressosExcedeCapacidadeMaxima()
    {
        // Arrange
        var nomeIngresso = "ingresso";
        var evento = EventoFactory.CriarEvento(capacidadeMaxima: 5);
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10);

        // Act
        var resultadoAtualizarEvento = evento.AdicionarIngresso(ingresso);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
                                .And.Satisfy(a => a.Description == ErrosEvento.QuantidadeTotalIngressosExcedeCapacidadeMaxima);
    }

    [Fact]
    public void AtualizarIngresso_ComErro_NomeJaExiste()
    {
        // Arrange
        var nomeIngresso = "ingresso";
        var evento = EventoFactory.CriarEvento();
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10);
        evento.AdicionarIngresso(ingresso);

        var ingressoErro = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10);

        // Act
        var resultadoAtualizarEvento = evento.AtualizarIngresso(ingressoErro);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
                                .And.Satisfy(a => a.Description == ErrosEvento.NomeIngressoJaExiste);
    }

    [Fact]
    public void AtualizarIngresso_ComErro_QuantidadeTotalIngressosExcedeCapacidadeMaxima()
    {
        // Arrange
        var evento = EventoFactory.CriarEvento(capacidadeMaxima: 11);
        var ingresso = Ingresso.Criar("ingresso", "descricao ingresso", 10, 10);
        evento.AdicionarIngresso(ingresso);

        var ingresso2 = Ingresso.Criar("ingresso 2", "descricao ingresso 2", 10, 10);

        // Act
        var resultadoAtualizarEvento = evento.AtualizarIngresso(ingresso2);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
                                .And.Satisfy(a => a.Description == ErrosEvento.QuantidadeTotalIngressosExcedeCapacidadeMaxima);
    }

    [Fact]
    public void RemoverIngresso_ComSucesso()
    {
        // Arrange
        var evento = EventoFactory.CriarEvento(capacidadeMaxima: 11);
        var ingresso = Ingresso.Criar("ingresso", "descricao ingresso", 10, 10);
        evento.AdicionarIngresso(ingresso);

        // Act
        var resultadoAtualizarEvento = evento.RemoverIngresso(ingresso);

        // Assert
        // Assert
        resultadoAtualizarEvento.IsError.Should().BeFalse();
    }
}