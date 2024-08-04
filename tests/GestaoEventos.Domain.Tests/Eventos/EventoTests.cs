﻿using FluentAssertions.Equivalency;
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
        var evento = EventoFactory.CriarEvento();
        var ingresso = Ingresso.Criar("ingresso", "descricação ingresso", 1, 10);

        // Act
        var resultadoAdicionarIngresso = evento.AdicionarIngresso(ingresso);

        // Assert
        resultadoAdicionarIngresso.IsError.Should().BeFalse();
        evento.Ingressos.Should().NotBeEmpty()
                                .And.HaveCount(1);
    }

    [Fact]
    public void AdicionarIngresso_ComErro_NomeIngressoJaExiste()
    {
        // Arrange
        var evento = EventoFactory.CriarEvento();
        var nomeIngresso = "ingresso";
        var ingresso = Ingresso.Criar(nomeIngresso, "descricação ingresso", 1, 10);
        evento.AdicionarIngresso(ingresso);

        // Act
        var resultadoAdicionarIngresso = evento.AdicionarIngresso(Ingresso.Criar(nomeIngresso, "descricação ingresso", 1, 10));

        // Assert
        resultadoAdicionarIngresso.IsError.Should().BeTrue();
        evento.Ingressos.Should().NotBeEmpty()
                                .And.HaveCount(1);
    }
}