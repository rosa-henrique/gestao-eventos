namespace GestaoEventos.Api.Endpoints.Auth.Request;

public record LoginRequest(string Email, string Senha)
{
}