using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Quartz;
using SWD.NextIntern.Service.Common.Behaviours;
using SWD.NextIntern.Service.Common.Validation;
using SWD.NextIntern.Service.Services.Quartzs;
using System.Reflection;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.Delete;
using SWD.NextIntern.Service.Services.CampaignService.Delete;
using SWD.NextIntern.Service.Services.EvaluationFormService.Delete;
using SWD.NextIntern.Service.Services.InternService.Delete;
using SWD.NextIntern.Service.Services.UniversityService.Delete;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Delete;
using SWD.NextIntern.Service.Services.FormCriteriaService.Delete;
using SWD.NextIntern.Service.Services.InternEvaluationService.Delete;
using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Delete;
using AutoMapper;
using SWD.NextIntern.Repository.Repositories.IRepositories;


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

            services.AddTransient<IRequestHandler<DeleteUniversityCommand, ResponseObject<string>>, DeleteUniversityCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteInternCommand, ResponseObject<string>>, DeleteInternCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteEvaluationFormCommand, ResponseObject<string>>, DeleteEvaluationFormCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteCampaignCommand, ResponseObject<string>>, DeleteCampaignCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteCampaignEvaluationCommand, ResponseObject<string>>, DeleteCampaignEvaluationCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteInternEvaluationCommand, ResponseObject<string>>, DeleteInternEvaluationCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteFormCriteriaCommand, ResponseObject<string>>, DeleteFormCriteriaCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteCampaignQuestionCommand, ResponseObject<string>>, DeleteCampaignQuestionCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteCampaignQuestionResponseCommand, ResponseObject<string>>, DeleteCampaignQuestionResponseCommandHandler>();

            services.AddTransient<DeleteCampaignCommandHandler>();
            services.AddTransient<DeleteUniversityCommandHandler>();
            services.AddTransient<DeleteInternCommandHandler>();
            services.AddTransient<DeleteEvaluationFormCommandHandler>();
            services.AddTransient<DeleteCampaignEvaluationCommandHandler>();
            services.AddTransient<DeleteCampaignCommandHandler>();
            services.AddTransient<DeleteInternEvaluationCommandHandler>();
            services.AddTransient<DeleteFormCriteriaCommandHandler>();
            services.AddTransient<DeleteCampaignQuestionCommandHandler>();
            services.AddTransient<DeleteCampaignQuestionResponseCommandHandler>();

            return services;
        }
    }
}
