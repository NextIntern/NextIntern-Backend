using NextIntern.API.Filters;
using NextIntern.Application;
using NextIntern.Application.Auth.ForgotPassword;
using NextIntern.Application.Auth.SignIn;
using NextIntern.Application.Auth.SignUp;
using NextIntern.Infrastructure;
//using SWD.NextIntern.API.Filters;
//using SWD.NextIntern.Repository;
using SWD.NextIntern.Service;
using SWD.NextIntern.Service.Common.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using SWD.NextIntern.Service.DTOs.Settings;

namespace NextIntern.API
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
            services.AddSwaggerGen();
            services.AddDistributedMemoryCache();

            //Register layer
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //Register layer
            //services.AddRepository(Configuration);
            //services.AddService(Configuration);

            //Register configuration
            services.AddApplication(Configuration);
            services.AddInfrastructure(Configuration);
            services.ConfigureApplicationSecurity(Configuration);
            services.AddScoped<SignUpCommandHandler>();
            services.AddScoped<SignInQueryHandler>();
            services.AddTransient<ForgotPasswordCommandHandler>();
            services.AddScoped<ForgotPasswordCommandHandler>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}