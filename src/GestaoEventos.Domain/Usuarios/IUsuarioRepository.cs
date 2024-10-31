using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Usuarios;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> BuscarPorEmail(string email);
}