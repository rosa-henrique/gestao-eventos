using EventFlow.Shared.Infrastructure.CurrentUserProvider;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace EventFlow.Shared.Infrastructure.Security.CurrentUserProvider;

public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        var id = Guid.Parse(GetSingleClaimValue(JwtRegisteredClaimNames.Sub));
        var nome = GetSingleClaimValue(JwtRegisteredClaimNames.Name);
        var email = GetSingleClaimValue(JwtRegisteredClaimNames.Email);

        return new CurrentUser(id, nome, email);
    }

    private string GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Single(claim => claim.Type == claimType)
            .Value;
}