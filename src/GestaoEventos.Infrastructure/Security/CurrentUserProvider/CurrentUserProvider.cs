using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Http;

namespace GestaoEventos.Infrastructure.Security.CurrentUserProvider;

public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        var id = Guid.Parse(GetSingleClaimValue("id"));
        var nome = GetSingleClaimValue(JwtRegisteredClaimNames.Name);

        return new CurrentUser(id, nome, string.Empty);
    }

    private string GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Single(claim => claim.Type == claimType)
            .Value;
}