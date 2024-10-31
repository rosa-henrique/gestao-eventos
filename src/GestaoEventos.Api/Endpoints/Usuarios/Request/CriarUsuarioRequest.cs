namespace GestaoEventos.Api.Endpoints.Usuarios.Request;

public record CriarUsuarioRequest(string Nome, string Email, int Permissao)
{
}