namespace GestaoEventos.Infrastructure.Security.CurrentUserProvider;

public record CurrentUser(Guid Id, string Nome, string Email);