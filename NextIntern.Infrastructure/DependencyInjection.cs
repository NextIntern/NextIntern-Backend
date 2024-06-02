using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextIntern.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NextIntern.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Register services for Infrastructure layer
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b =>
                    {
                        b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
            });

            return services;
        }
    }
}
