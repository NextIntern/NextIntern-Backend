using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextIntern.Application.Common.Behaviours;
using System.Reflection;

namespace NextIntern.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            //Register services for Application layer
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
