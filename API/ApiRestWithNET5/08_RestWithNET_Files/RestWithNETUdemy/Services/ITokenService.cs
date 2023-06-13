using System.Collections.Generic;
using System.Security.Claims;

namespace RestWithNETUdemy.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefrashToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
