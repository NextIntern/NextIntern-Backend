using SWD.NextIntern.API.Filters;
using SWD.NextIntern.Repository;
using SWD.NextIntern.Service;
using SWD.NextIntern.Service.Auth.ForgotPassword;
using SWD.NextIntern.Service.Auth.SignIn;
using SWD.NextIntern.Service.Auth.SignUp;
using SWD.NextIntern.Service.Common.Configuration;

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