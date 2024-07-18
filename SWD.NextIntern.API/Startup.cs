using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.OpenApi.Models;
using SWD.NextIntern.API.Filters;
using SWD.NextIntern.Repository;
using SWD.NextIntern.Service;
using SWD.NextIntern.Service.Common.Configuration;
using SWD.NextIntern.Service.Common.Mappings;


namespace SWD.NextIntern.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(otp =>
            {
                otp.Filters.Add<ExceptionFilter>();
            });

            services.AddEndpointsApiExplorer();
            services.AddDistributedMemoryCache();

            services.ConfigureApplicationSecurity(Configuration);

            services.AddService(Configuration);
            services.AddRepository(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NextIntern API", Version = "v1" });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT Token.",
                };
                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "172802685733-8o2atnnhq7vqq2da3gjb9g4u60nq17au.apps.googleusercontent.com";
                googleOptions.ClientSecret = "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";
                googleOptions.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
                googleOptions.CorrelationCookie.SameSite = SameSiteMode.None;
                //googleOptions.SaveTokens = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            app.UseSwagger();
            app.UseSwaggerUI();
            // }

            app.UseHttpsRedirection();
            app.UseRouting();

            //app.UseMiddleware<JwtMiddleware>();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowOrigin");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}

