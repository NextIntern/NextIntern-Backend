using Microsoft.AspNetCore.Http;
using SWD.NextIntern.Service.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IJwtService jwtService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var principal = jwtService.GetPrincipal(token);
            if (principal != null)
            {
                context.Items["User"] = principal;
            }
        }

        await _next(context);
    }
}
