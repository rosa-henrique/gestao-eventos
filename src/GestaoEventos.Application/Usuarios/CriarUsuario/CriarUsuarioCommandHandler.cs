using ErrorOr;

using GestaoEventos.Domain.Eventos;
using GestaoEventos.Domain.Usuarios;

using MediatR;

namespace GestaoEventos.Application.Usuarios.CriarUsuario;

public class CriarUsuarioCommandHandler(IUsuarioRepository repository)
    : IRequestHandler<CriarUsuarioCommand, ErrorOr<Usuario>>
{
    public async Task<ErrorOr<Usuario>> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuarioPorEmail = await repository.BuscarPorEmail(request.Email);
        if (usuarioPorEmail is not null)
        {
            return Error.Conflict(description: ErrosUsuario.JaExisteEmail);
        }

        var resultadoCriarUsuario = Usuario.Criar(request.Nome, request.Email, Permissao.FromValue(request.Permissao));
        if (resultadoCriarUsuario.IsError)
        {
            return resultadoCriarUsuario.Errors;
        }

        var usuario = resultadoCriarUsuario.Value;
        repository.Adicionar(usuario);
        await repository.SaveChangesAsync(cancellationToken);

        return usuario;
    }
}