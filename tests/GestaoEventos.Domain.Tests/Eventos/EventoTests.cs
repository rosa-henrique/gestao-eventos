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

            Assert.True(resultadoEvento.IsError);
            Assert.Equal(ErrosEvento.CapacidadeInvalida, resultadoEvento.Errors.FirstOrDefault().Description);
        }
    }
}