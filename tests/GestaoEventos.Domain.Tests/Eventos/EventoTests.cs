using GestaoEventos.Domain.Eventos;

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
            var localizacao = "Rua teste";
            var capacidadeMaxima = 5;

            // Act
            var resultadoEvento = Evento.Criar(nome, dataHora, localizacao, capacidadeMaxima);

            // Assert
            Assert.False(resultadoEvento.IsError);
            Assert.Equal(resultadoEvento.Value.Detalhes.Nome, nome);
            Assert.Equal(resultadoEvento.Value.Detalhes.DataHora, dataHora);
            Assert.Equal(resultadoEvento.Value.Detalhes.Localizacao, localizacao);
            Assert.Equal(resultadoEvento.Value.Detalhes.CapacidadeMaxima, capacidadeMaxima);
        }

        [Fact]
        public void CriarEvento_ComErro_DataRetroativa()
        {
            // Arrange
            var nome = "teste";
            var dataHora = DateTime.UtcNow.AddDays(-7);
            var localizacao = "Rua teste";
            var capacidadeMaxima = 5;

            // Act
            var resultadoEvento = Evento.Criar(nome, dataHora, localizacao, capacidadeMaxima);

            // Assert
            Assert.True(resultadoEvento.IsError);
            Assert.Equal(ErrosEvento.DataRetroativa, resultadoEvento.Errors.FirstOrDefault().Description);
        }

        [Fact]
        public void CriarEvento_ComErro_CapacidadeInvalida()
        {
            // Arrange
            var nome = "teste";
            var dataHora = DateTime.UtcNow.AddDays(7);
            var localizacao = "Rua teste";
            var capacidadeMaxima = 0;

            // Act
            var resultadoEvento = Evento.Criar(nome, dataHora, localizacao, capacidadeMaxima);

            // Assert
            Assert.True(resultadoEvento.IsError);
            Assert.Equal(ErrosEvento.CapacidadeInvalida, resultadoEvento.Errors.FirstOrDefault().Description);
        }

        [Fact]
        public void AtualizarEvento_ComSucesso()
        {
            // Arrange
            var nome = "teste";
            var dataHora = DateTime.UtcNow.AddDays(1);
            var localizacao = "Rua teste";
            var capacidadeMaxima = 5;
            var evento = Evento.Criar(string.Empty, dataHora, string.Empty, 1).Value;

            // Act
            var resultadoEvento = evento.Atualizar(nome, evento.Detalhes.DataHora, localizacao, capacidadeMaxima);

            // Assert
            Assert.False(resultadoEvento.IsError);
            Assert.Equal(evento.Detalhes.Nome, nome);
            Assert.Equal(evento.Detalhes.DataHora, dataHora);
            Assert.Equal(evento.Detalhes.Localizacao, localizacao);
            Assert.Equal(evento.Detalhes.CapacidadeMaxima, capacidadeMaxima);
        }

        [Fact]
        public void AtualizarEvento_ComErro_DataRetroativa()
        {
            // Arrange
            var evento = Evento.Criar(string.Empty, DateTime.UtcNow.AddDays(1), string.Empty, 1).Value;

            // Act
            var resultadoEvento = evento.Atualizar(string.Empty, DateTime.UtcNow.AddDays(-1), string.Empty, 1);

            // Assert
            Assert.True(resultadoEvento.IsError);
            Assert.Equal(ErrosEvento.DataRetroativa, resultadoEvento.Errors.FirstOrDefault().Description);
        }

        [Fact]
        public void AtualizarEvento_ComErro_CapacidadeInvalida()
        {
            // Arrange
            var evento = Evento.Criar(string.Empty, DateTime.UtcNow.AddDays(1), string.Empty, 1).Value;

            // Act
            var resultadoEvento = evento.Atualizar(string.Empty, DateTime.UtcNow.AddDays(1), string.Empty, 0);

            // Assert
            Assert.True(resultadoEvento.IsError);
            Assert.Equal(ErrosEvento.CapacidadeInvalida, resultadoEvento.Errors.FirstOrDefault().Description);
        }
    }
}