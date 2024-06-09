using System.Security.Claims;

namespace SWD.NextIntern.Service.Common.Interfaces
{
    public interface IJwtService
    {
        Task<string> CreateToken(string ID, string roles);
        Task<string> GenerateRefreshToken(string ID, string roles);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        ClaimsPrincipal GetPrincipal(string token);
    }
}
