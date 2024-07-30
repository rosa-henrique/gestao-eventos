using System.Reflection;

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
            var evento = Evento.Criar("1", dataHora.AddDays(1), "Rua 1", capacidadeMaxima).Value;

            // Act
            var resultadoAtualizarEvento = evento.Atualizar(nome, dataHora, localizacao, capacidadeMaxima);

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
            var evento = Evento.Criar("1", dataHora.AddDays(1), "Rua 1", DetalhesEvento.CapacidadeMinima + 5).Value;

            // Act
            var resultadoAtualizarEvento = evento.Atualizar(nome, dataHora, localizacao, capacidadeMaxima);

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
            var evento = Evento.Criar("1", DateTime.UtcNow.AddDays(7), "Rua 1", capacidadeMaxima).Value;

            // Act
            var resultadoAtualizarEvento = evento.Atualizar(nome, dataHora, localizacao, capacidadeMaxima);

            // Assert
            Assert.True(resultadoAtualizarEvento.IsError);
            Assert.Equal(ErrosEvento.DataRetroativa, resultadoAtualizarEvento.Errors.First().Description);
        }

        [Fact]
        public void AtualizarEvento_ComErro_EventoJaPassou()
        {
            // Arrange
            var nome = "teste";
            var dataHora = DateTime.UtcNow.AddDays(-7);
            var localizacao = "123";
            var capacidadeMaxima = DetalhesEvento.CapacidadeMinima + 5;
            var eventoType = typeof(Evento);
            var constructor = eventoType.GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(string), typeof(DateTime), typeof(string), typeof(int)],
                null)!;

            var eventoPassado = (Evento)constructor.Invoke(["Evento Passado", DateTime.UtcNow.AddDays(-8), "Localização", 100]);

            var resultadoAtualizarEvento = eventoPassado.Atualizar(nome, dataHora, localizacao, capacidadeMaxima);

            // Assert
            Assert.True(resultadoAtualizarEvento.IsError);
            Assert.Equal(ErrosEvento.NaoAlterarEventoPassado, resultadoAtualizarEvento.Errors.First().Description);
        }
    }
}