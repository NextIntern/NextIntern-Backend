using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Quartz;
using SWD.NextIntern.Service.Common.Behaviours;
using SWD.NextIntern.Service.Common.Validation;
using SWD.NextIntern.Service.Services.Quartzs;
using System.Reflection;


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
                cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidatorProvider, ValidatorProvider>();

            // Add Quartz services
            services.AddQuartz(q =>
            {
                // Use the job factory that uses Microsoft DI
                //q.UseMicrosoftDependencyInjectionJobFactory();

                // Register the job
                q.AddJob<UpdateCampaignStateJob>(opts => opts.WithIdentity("UpdateCampaignStateJob"));

                // Register the trigger
                q.AddTrigger(opts => opts
                    .ForJob("UpdateCampaignStateJob")
                    .WithIdentity("UpdateCampaignStateJob-trigger")
                    .WithCronSchedule("0/5 * * * * ?")); // Run every 5 seconds for example
            });

            // Add Quartz.NET hosted service
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            return services;
        }
    }
}
