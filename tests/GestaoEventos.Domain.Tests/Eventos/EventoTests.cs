using GestaoEventos.Domain.Eventos;
using GestaoEventos.TestCommon.Eventos;

namespace GestaoEventos.Domain.Tests.Eventos
{
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
            Assert.False(resultadoEvento.IsError);
            var evento = resultadoEvento.Value;
            Assert.Equal(nome, evento.Detalhes.Nome);
            Assert.Equal(dataHora, evento.Detalhes.DataHora);
            Assert.Equal(localizacao, evento.Detalhes.Localizacao);
            Assert.Equal(capacidadeMaxima, evento.Detalhes.CapacidadeMaxima);
            Assert.Equal(StatusEvento.Pendente, evento.Detalhes.Status);
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
            Assert.True(resultadoEvento.IsError);
            Assert.Equal(ErrosEvento.CapacidadeInvalida, resultadoEvento.Errors.First().Description);
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
            Assert.True(resultadoEvento.IsError);
            Assert.Equal(ErrosEvento.DataRetroativa, resultadoEvento.Errors.First().Description);
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
            Assert.False(resultadoAtualizarEvento.IsError);
            Assert.Equal(nome, evento.Detalhes.Nome);
            Assert.Equal(dataHora, evento.Detalhes.DataHora);
            Assert.Equal(localizacao, evento.Detalhes.Localizacao);
            Assert.Equal(capacidadeMaxima, evento.Detalhes.CapacidadeMaxima);
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
            Assert.True(resultadoAtualizarEvento.IsError);
            Assert.Equal(ErrosEvento.CapacidadeInvalida, resultadoAtualizarEvento.Errors.First().Description);
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
            Assert.True(resultadoAtualizarEvento.IsError);
            Assert.Equal(ErrosEvento.DataRetroativa, resultadoAtualizarEvento.Errors.First().Description);
        }

        [Fact]
        public void AtualizarEvento_ComErro_EventoJaPassou()
        {
            // Arrange
            var eventoPassado = EventoFactory.CriarEvento(dataHora: DateTime.Now.AddDays(-8));

            // Act
            var resultadoAtualizarEvento = eventoPassado.Atualizar("nome", DateTime.Now, "localizacao", 5, StatusEvento.Pendente);

            // Assert
            Assert.True(resultadoAtualizarEvento.IsError);
            Assert.Equal(ErrosEvento.NaoAlterarEventoPassado, resultadoAtualizarEvento.Errors.First().Description);
        }

        [Fact]
        public void CancelarEvento_ComSucesso()
        {
            // Arrange
            var evento = EventoFactory.CriarEvento();

            // Act
            var resultadoAtualizarEvento = evento.Cancelar();

            // Assert
            Assert.False(resultadoAtualizarEvento.IsError);
            Assert.Equal(StatusEvento.Cancelado, evento.Detalhes.Status);
        }

        [Fact]
        public void CancelarEvento_ComErro_EventoJaPassou()
        {
            // Arrange
            var eventoPassado = EventoFactory.CriarEvento(dataHora: DateTime.Now.AddDays(-8));

            var resultadoAtualizarEvento = eventoPassado.Cancelar();

            // Assert
            Assert.True(resultadoAtualizarEvento.IsError);
            Assert.Equal(ErrosEvento.NaoAlterarEventoPassado, resultadoAtualizarEvento.Errors.First().Description);
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
            Assert.False(resultadoAtualizarEvento.IsError);
            Assert.Equal(novoStatus, evento.Detalhes.Status);
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
            Assert.True(resultadoAtualizarEvento.IsError);
            Assert.Equal(msgErro, resultadoAtualizarEvento.Errors.First().Description);
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
            Assert.True(resultadoAtualizarEvento.IsError);
            Assert.Equal(msgErro, resultadoAtualizarEvento.Errors.First().Description);
        }
    }
}