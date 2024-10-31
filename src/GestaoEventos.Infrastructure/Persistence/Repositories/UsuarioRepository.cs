using GestaoEventos.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;

namespace GestaoEventos.Infrastructure.Persistence.Repositories;

public class UsuarioRepository(AppDbContext context) : Repository<Usuario>(context), IUsuarioRepository
{
    public Task<Usuario?> BuscarPorEmail(string email)
    {
        return _dbSet.FirstOrDefaultAsync(u => string.Equals(u.Email.ToLower(), email.ToLower()));
    }
}