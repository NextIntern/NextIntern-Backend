
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SWD.NextIntern.Service.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using SWD.NextIntern.Service.Services;

namespace SWD.NextIntern.Service.Common.Configuration
{
    public static class ApplicationSecurityConfiguration
    {
        public static IServiceCollection ConfigureApplicationSecurity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddHttpContextAccessor();

            var authority = configuration["Security:Bearer:Authority"];
            var audience = configuration["Security:Bearer:Audience"];
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = audience;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            //services.AddControllers();
            services.AddAuthorization(ConfigureAuthorization);
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    b =>
                    {
                        b.SetIsOriginAllowed(host => true)
                            //.WithOrigins("http://localhost:3000", "https://nextintern.tech")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

            return services;
        }


        private static void ConfigureAuthorization(AuthorizationOptions options)
        {
            //Configure policies and other authorization options here. For example:
            //options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("role", "employee"));
            //options.AddPolicy("AdminOnly", policy => policy.RequireClaim("role", "Admin"));
            options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("role", "Admin"));
            options.AddPolicy("UserPolicy", policy => policy.RequireClaim("role", "User"));
            //options.AddPolicy("UserPolicy", policy => policy.RequireClaim("role", "User"));
            //options.AddPolicy("UserPolicy", policy =>
            //{
            //    policy.RequireAuthenticatedUser();
            //    policy.RequireRole("User");
            //});

            //options.AddPolicy("AdminPolicy", policy =>
            //     policy.RequireRole("Admin"));
        }
    }
}
