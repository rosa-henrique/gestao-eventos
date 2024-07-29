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
            var evento = MockEvento.Novo(nome, dataHora, localizacao, capacidadeMaxima);

            // Assert
            Assert.Equal(evento.Detalhes.Nome, nome);
            Assert.Equal(evento.Detalhes.DataHora, dataHora);
            Assert.Equal(evento.Detalhes.Localizacao, localizacao);
            Assert.Equal(evento.Detalhes.CapacidadeMaxima, capacidadeMaxima);
        }
    }
}