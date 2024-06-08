using System.Security.Claims;

namespace SWD.NextIntern.Service.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateRefreshToken(string ID, string roles);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
