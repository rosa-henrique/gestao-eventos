using GestaoEventos.Domain.Usuarios;

namespace GestaoEventos.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid id, string nome, string email, Permissao permissao);
}