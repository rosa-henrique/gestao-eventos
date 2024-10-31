using ErrorOr;

using GestaoEventos.Domain.Usuarios;

using MediatR;

namespace GestaoEventos.Application.Usuarios.CriarUsuario;

public record CriarUsuarioCommand(string Nome, string Email, int Permissao) : IRequest<ErrorOr<Usuario>>
{
}