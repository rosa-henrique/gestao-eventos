using ErrorOr;

using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos
{
    public class DetalhesEvento : ValueObject
    {
        public string Nome { get; private set; } = null!;
        public DateTime DataHora { get; private set; }
        public string Localizacao { get; private set; } = null!;
        public int CapacidadeMaxima { get; private set; }

        internal static ErrorOr<DetalhesEvento> Criar(string nome, DateTime dataHora, string localizacao, int capacidadeMaxima)
        {
            if (dataHora < DateTime.UtcNow)
            {
                return Error.Conflict(description: ErrosEvento.DataRetroativa);
            }

            if (capacidadeMaxima <= 0)
            {
                return Error.Failure(description: ErrosEvento.CapacidadeInvalida);
            }

            return new DetalhesEvento
            {
                Nome = nome,
                DataHora = dataHora,
                Localizacao = localizacao,
                CapacidadeMaxima = capacidadeMaxima,
            };
        }

        private DetalhesEvento() { }

        // Implementação de ValueObject
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Nome;
            yield return DataHora;
            yield return Localizacao;
            yield return CapacidadeMaxima;
        }
    }
}