
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SWD.NextIntern.Service.Common.Behaviours;
using FluentValidation;
using SWD.NextIntern.Service.Common.Validation;
using System.Reflection;
using SWD.NextIntern.Service.Auth.ForgotPassword;
using SWD.NextIntern.Service.Auth.ResetPassword;
using SWD.NextIntern.Service.Auth.SignIn;
using SWD.NextIntern.Service.Auth.SignUp;
using SWD.NextIntern.Service.Services.Auth.RefreshToken;

namespace SWD.NextIntern.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), lifetime: ServiceLifetime.Transient);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                //cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidatorProvider, ValidatorProvider>();
            services.AddScoped<SignUpCommandHandler>();
            services.AddScoped<SignInQueryHandler>();
            services.AddTransient<ForgotPasswordQueryHandler>();
            services.AddScoped<ForgotPasswordQueryHandler>();
            services.AddTransient<ResetPasswordCommandHandler>();
            services.AddScoped<ResetPasswordCommandHandler>();
            services.AddTransient<RefreshTokenCommandHandler>();
            services.AddScoped<RefreshTokenCommandHandler>();

            return services;
        }
    }
}
