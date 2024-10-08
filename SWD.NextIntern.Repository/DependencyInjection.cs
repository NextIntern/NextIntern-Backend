using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SWD.NextIntern.Repository.Common;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using AutoMapper;

namespace SWD.NextIntern.Repository
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {

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

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IUniversityRepository, UniversityRepository>();
            services.AddScoped<ICampaignEvaluationRepository, CampaignEvaluationRepository>();
            services.AddScoped<IInternEvaluationRepository, InternEvaluationRepository>();
            services.AddScoped<IEvaluationFormRepository, EvaluationFormRepository>();
            services.AddScoped<IFormCriteriaRepository, FormCriteriaRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IInternEvaluationCriteriaRepository, InternEvaluationCriteriaRepository>();
            services.AddScoped<ICampaignQuestionRepository, CampaignQuestionRepository>();
            services.AddScoped<ICampaignQuestionResponseRepository, CampaignQuestionResponseRepository>();

            return services;
        }
    }
}
