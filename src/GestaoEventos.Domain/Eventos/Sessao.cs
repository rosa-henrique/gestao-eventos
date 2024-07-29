//using ErrorOr;

//using GestaoEventos.Domain.Common;

//namespace GestaoEventos.Domain.Eventos;

//public sealed class Sessao : Entity
//{
//    private Sessao(string nome, DateTime inicio, DateTime fim)
//    {
//        Nome = nome;
//        Inicio = inicio;
//        Fim = fim;
//    }

//    internal static ErrorOr<Sessao> Criar(string nome, DateTime inicio, DateTime fim)
//    {
//        return inicio < fim
//            ? Error.Failure(description: "validar mensagem")
//            : new Sessao(nome, inicio, fim);
//    }

//    public string Nome { get; private set; } = null!;
//    public DateTime Inicio { get; private set; }
//    public DateTime Fim { get; private set; }

//    // Métodos de domínio
//}