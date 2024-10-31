using ErrorOr;

using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Usuarios;

public class Usuario : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public Permissao Permissao { get; private set; }

    private Usuario(string nome, string email, Permissao permissao)
    {
        Nome = nome;
        Email = email;
        Permissao = permissao;
    }

    public static ErrorOr<Usuario> Criar(string nome, string email, Permissao permissao)
    {
        return new Usuario(nome, email, permissao);
    }
}