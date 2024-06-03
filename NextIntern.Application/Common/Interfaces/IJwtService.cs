using System.Security.Claims;

namespace NextIntern.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(string ID, string roles);
        string GenerateRefreshToken(string ID, string roles);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}