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
        const string nome = "teste";
        var dataHoraInicio = DateTime.UtcNow.AddDays(7);
        var dataHoraFim = DateTime.UtcNow.AddDays(7).AddDays(6);
        const string localizacao = "123";
        const int capacidadeMaxima = 5;

        // Act
        var resultadoEvento = Evento.Criar(nome, dataHoraInicio, dataHoraFim, localizacao, capacidadeMaxima);

        // Assert
        resultadoEvento.IsError.Should().BeFalse();
        resultadoEvento.Value.Detalhes.Should().NotBeNull()
            .And.BeEquivalentTo(new
            {
                Nome = nome,
                DataHoraInicio = dataHoraInicio,
                DataHoraFim = dataHoraFim,
                Localizacao = localizacao,
                CapacidadeMaxima = capacidadeMaxima,
                Status = StatusEvento.Pendente,
            });
    }

    [Fact]
    public void CriarEvento_ComErro_CapacidadeInvalida()
    {
        // Arrange
        const string nome = "teste";
        var dataHoraInicio = DateTime.UtcNow.AddDays(7);
        var dataHoraFim = DateTime.UtcNow.AddDays(7).AddHours(6);
        const string localizacao = "123";
        const int capacidadeMaxima = DetalhesEvento.CapacidadeMinima - 5;

        // Act
        var resultadoEvento = Evento.Criar(nome, dataHoraInicio, dataHoraFim, localizacao, capacidadeMaxima);

        // Assert
        resultadoEvento.IsError.Should().BeTrue();
        resultadoEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.CapacidadeInvalida);
    }

    [Fact]
    public void CriarEvento_ComErro_DataRetroativa()
    {
        // Arrange
        const string nome = "teste";
        var dataHoraInicio = DateTime.UtcNow.AddDays(-7);
        var dataHoraFim = DateTime.UtcNow.AddDays(7).AddHours(6);
        const string localizacao = "123";
        const int capacidadeMaxima = DetalhesEvento.CapacidadeMinima + 5;

        // Act
        var resultadoEvento = Evento.Criar(nome, dataHoraInicio, dataHoraFim, localizacao, capacidadeMaxima);

        // Assert
        resultadoEvento.IsError.Should().BeTrue();
        resultadoEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.DataRetroativa);
    }

    [Fact]
    public void CriarEvento_ComErro_DataFinalMenorIgualFinal()
    {
        // Arrange
        const string nome = "teste";
        var dataHoraInicio = DateTime.UtcNow.AddDays(7);
        var dataHoraFim = DateTime.UtcNow.AddDays(7).AddHours(-1);
        const string localizacao = "123";
        const int capacidadeMaxima = DetalhesEvento.CapacidadeMinima + 5;

        // Act
        var resultadoEvento = Evento.Criar(nome, dataHoraInicio, dataHoraFim, localizacao, capacidadeMaxima);

        // Assert
        resultadoEvento.IsError.Should().BeTrue();
        resultadoEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.DataFinalMenorIgualFinal);
    }

    [Fact]
    public void AtualizarEvento_ComSucesso()
    {
        // Arrange
        const string nome = "teste";
        var dataHoraInicio = DateTime.UtcNow.AddDays(7);
        var dataHoraFim = DateTime.UtcNow.AddDays(7).AddHours(6);
        const string localizacao = "123";
        const int capacidadeMaxima = DetalhesEvento.CapacidadeMinima + 5;
        var evento = EventoFactory.CriarEvento();

        // Act
        var resultadoAtualizarEvento = evento.Atualizar(nome, dataHoraInicio, dataHoraFim, localizacao,
            capacidadeMaxima, StatusEvento.Pendente);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeFalse();
        evento.Detalhes.Should().NotBeNull()
            .And.BeEquivalentTo(new
            {
                Nome = nome,
                DataHoraInicio = dataHoraInicio,
                DataHoraFim = dataHoraFim,
                Localizacao = localizacao,
                CapacidadeMaxima = capacidadeMaxima,
            });
    }

    [Fact]
    public void AtualizarEvento_ComErro_CapacidadeInvalida()
    {
        // Arrange
        const string nome = "teste";
        var dataHoraInicio = DateTime.UtcNow.AddDays(7);
        var dataHoraFim = DateTime.UtcNow.AddDays(7).AddDays(6);
        const string localizacao = "123";
        const int capacidadeMaxima = DetalhesEvento.CapacidadeMinima - 5;
        var evento = EventoFactory.CriarEvento();

        // Act
        var resultadoAtualizarEvento = evento.Atualizar(nome, dataHoraInicio, dataHoraFim, localizacao,
            capacidadeMaxima, StatusEvento.Pendente);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.CapacidadeInvalida);
    }

    [Fact]
    public void AtualizarEvento_ComErro_DataRetroativa()
    {
        // Arrange
        const string nome = "teste";
        var dataHoraInicio = DateTime.UtcNow.AddDays(-7);
        var dataHoraFim = DateTime.UtcNow.AddDays(7).AddDays(6);
        const string localizacao = "123";
        const int capacidadeMaxima = DetalhesEvento.CapacidadeMinima + 5;
        var evento = EventoFactory.CriarEvento();

        // Act
        var resultadoAtualizarEvento = evento.Atualizar(nome, dataHoraInicio, dataHoraFim, localizacao,
            capacidadeMaxima, StatusEvento.Pendente);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.DataRetroativa);
    }

    [Fact]
    public void AtualizarEvento_ComErro_EventoJaPassou()
    {
        // Arrange
        var eventoPassado = EventoFactory.CriarEvento(dataHoraInicio: DateTime.Now.AddDays(-8));

        // Act
        var resultadoAtualizarEvento =
            eventoPassado.Atualizar("nome", DateTime.Now, DateTime.Now, "localizacao", 5, StatusEvento.Pendente);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.NaoAlterarEventoPassado);
    }

    [Fact]
    public void AtualizarEvento_ComErro_ConflitoSessoes()
    {
        // Arrange
        var evento = EventoFactory.CriarEvento();
        var sessao = SessaoFactory.CriarSessao(dataHoraInicio: evento.Detalhes.DataHoraInicio,
            dataHoraFim: evento.Detalhes.DataHoraInicio.AddHours(2));
        evento.AdicionarSessao(sessao);

        // Act
        var resultadoAtualizarEvento = evento.Atualizar(evento.Detalhes.Nome, sessao.DataHoraInicio.AddMinutes(4),
            evento.Detalhes.DataHoraFim, evento.Detalhes.Localizacao, evento.Detalhes.CapacidadeMaxima,
            evento.Detalhes.Status);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.ConflitoSessoes);
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
        var eventoPassado = EventoFactory.CriarEvento(dataHoraInicio: DateTime.Now.AddDays(-8));

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
        const string nomeIngresso = "ingresso";
        var evento = EventoFactory.CriarEvento();
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10);

        // Act
        var resultadoAtualizarEvento = evento.AdicionarIngresso(ingresso.Value);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeFalse();
        evento.Ingressos.Should().NotBeEmpty()
            .And.Satisfy(a => a.Tipo.Nome == nomeIngresso);
    }

    [Fact]
    public void AdicionarIngresso_ComErro_NaoPermiteAdicaoIngresso()
    {
        // Arrange
        const string nomeIngresso = "ingresso";
        var statusEvento = StatusEvento.Cancelado;
        var evento = EventoFactory.CriarEvento(status: statusEvento);
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10);
        var msgErro = string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, statusEvento);

        // Act
        var resultadoAtualizarEvento = evento.AdicionarIngresso(ingresso.Value);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == msgErro);
    }

    [Fact]
    public void AdicionarIngresso_ComErro_NomeIngressoJaExiste()
    {
        // Arrange
        const string nomeIngresso = "ingresso";
        var evento = EventoFactory.CriarEvento();
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10);
        evento.AdicionarIngresso(ingresso.Value);
        var novoIngresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10);

        // Act
        var resultadoAtualizarEvento = evento.AdicionarIngresso(novoIngresso.Value);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.NomeIngressoJaExiste);
    }

    [Fact]
    public void AdicionarIngresso_ComErro_QuantidadeTotalIngressosExcedeCapacidadeMaxima()
    {
        // Arrange
        const string nomeIngresso = "ingresso";
        var evento = EventoFactory.CriarEvento(capacidadeMaxima: 10);
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 11);

        // Act
        var resultadoAtualizarEvento = evento.AdicionarIngresso(ingresso.Value);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.QuantidadeTotalIngressosExcedeCapacidadeMaxima);
    }

    [Fact]
    public void AtualizarIngresso_ComSucesso()
    {
        // Arrange
        const string nomeIngresso = "ingresso";
        const string novoNomeIngresso = "ingresso alterado";
        var evento = EventoFactory.CriarEvento();
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10).Value;
        evento.AdicionarIngresso(ingresso);
        ingresso.Alterar(novoNomeIngresso, ingresso.Tipo.Descricao, ingresso.Preco, ingresso.Quantidade);

        // Act
        var resultadoAtualizarEvento = evento.AtualizarIngresso(ingresso);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeFalse();
        ingresso.Tipo.Nome.Should().BeSameAs(novoNomeIngresso);
    }

    [Fact]
    public void AtualizarIngresso_ComErro_NaoPermiteAdicaoIngresso()
    {
        // Arrange
        const string nomeIngresso = "ingresso";
        var statusEvento = StatusEvento.Cancelado;
        var evento = EventoFactory.CriarEvento();
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10).Value;
        evento.AtualizarStatus(statusEvento);
        var msgErro = string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, statusEvento);

        // Act
        var resultadoAtualizarEvento = evento.AtualizarIngresso(ingresso);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == msgErro);
    }

    [Fact]
    public void AtualizarIngresso_ComErro_NomeIngressoJaExiste()
    {
        // Arrange
        const string nomeIngresso = "ingresso";
        var statusEvento = StatusEvento.Cancelado;
        var evento = EventoFactory.CriarEvento();
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10).Value;
        evento.AdicionarIngresso(ingresso);
        var novoIngresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10).Value;

        // Act
        var resultadoAtualizarEvento = evento.AtualizarIngresso(novoIngresso);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.NomeIngressoJaExiste);
    }

    [Fact]
    public void AtualizarIngresso_ComErro_QuantidadeTotalIngressosExcedeCapacidadeMaxima()
    {
        // Arrange
        const string nomeIngresso = "ingresso";
        var evento = EventoFactory.CriarEvento();
        var ingresso = Ingresso.Criar(nomeIngresso, "descricao ingresso", 10, 10).Value;
        evento.AdicionarIngresso(ingresso);
        ingresso.Alterar(ingresso.Tipo.Nome, ingresso.Tipo.Descricao, ingresso.Preco,
            evento.Detalhes.CapacidadeMaxima + 1);

        // Act
        var resultadoAtualizarEvento = evento.AtualizarIngresso(ingresso);

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
        var ingresso = Ingresso.Criar("ingresso", "descricao ingresso", 10, 10).Value;
        evento.AdicionarIngresso(ingresso);

        // Act
        var resultadoRemoverEvento = evento.RemoverIngresso(ingresso);

        // Assert
        resultadoRemoverEvento.IsError.Should().BeFalse();
    }

    [Fact]
    public void RemoverIngresso_ComErro_NaoPermiteAdicaoIngresso()
    {
        // Arrange
        var evento = EventoFactory.CriarEvento(capacidadeMaxima: 11);
        var statusEvento = StatusEvento.Cancelado;
        var ingresso = Ingresso.Criar("ingresso", "descricao ingresso", 10, 10).Value;
        evento.AdicionarIngresso(ingresso);
        evento.AtualizarStatus(statusEvento);
        var msgErro = string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, statusEvento);

        // Act
        var resultadoRemoverEvento = evento.RemoverIngresso(ingresso);

        // Assert
        resultadoRemoverEvento.IsError.Should().BeTrue();
        resultadoRemoverEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == msgErro);
    }

    [Fact]
    public void AdicionarSessao_Sucesso()
    {
        // Arrange
        const string nome = "teste";
        var evento = EventoFactory.CriarEvento();
        var sessao = SessaoFactory.CriarSessao(nome: nome, dataHoraInicio: evento.Detalhes.DataHoraInicio,
            dataHoraFim: evento.Detalhes.DataHoraInicio.AddHours(2));

        // Act
        var resultadoAdicionarSessao = evento.AdicionarSessao(sessao);

        // Assert
        resultadoAdicionarSessao.IsError.Should().BeFalse();
    }

    [Fact]
    public void AdicionarSessao_ComErro_NaoPermiteAdicaoIngresso()
    {
        // Arrange
        const string nome = "ingresso";
        var statusEvento = StatusEvento.Cancelado;
        var evento = EventoFactory.CriarEvento();
        var sessao = SessaoFactory.CriarSessao(nome: nome, dataHoraInicio: evento.Detalhes.DataHoraInicio,
            dataHoraFim: evento.Detalhes.DataHoraInicio.AddHours(2));
        evento.AtualizarStatus(statusEvento);
        var msgErro = string.Format(ErrosEvento.NaoPermiteAdicaoIngresso, statusEvento);

        // Act
        var resultadoAtualizarEvento = evento.AdicionarSessao(sessao);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == msgErro);
    }

    [Fact]
    public void AdicionarSessao_ComErro_DataSessaoForaIntervaloEvento()
    {
        // Arrange
        const string nome = "teste";
        var evento = EventoFactory.CriarEvento();
        var sessao = SessaoFactory.CriarSessao(nome: nome, dataHoraInicio: evento.Detalhes.DataHoraInicio.AddHours(-2),
            dataHoraFim: evento.Detalhes.DataHoraInicio.AddHours(2));

        // Act
        var resultadoAtualizarEvento = evento.AdicionarSessao(sessao);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.DataSessaoForaIntervaloEvento);
    }

    [Fact]
    public void AdicionarSessao_ComErro_ConflitoDataHoraSessao()
    {
        // Arrange
        const string nome = "teste";
        var evento = EventoFactory.CriarEvento();
        var sessao = SessaoFactory.CriarSessao(nome: nome, dataHoraInicio: evento.Detalhes.DataHoraInicio,
            dataHoraFim: evento.Detalhes.DataHoraInicio.AddHours(2));
        evento.AdicionarSessao(sessao);
        var novaSessao = SessaoFactory.CriarSessao(dataHoraInicio: sessao.DataHoraInicio,
            dataHoraFim: sessao.DataHoraInicio.AddMinutes(10));

        // Act
        var resultadoAtualizarEvento = evento.AdicionarSessao(novaSessao);

        // Assert
        resultadoAtualizarEvento.IsError.Should().BeTrue();
        resultadoAtualizarEvento.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosEvento.ConflitoDataHoraSessao);
    }
}